using Domain.Auth;
using Domain.Exceptions;
using LanguageExt.Common;
using Persistanse;

namespace Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IHashService _hashService;
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AuthService(IHashService hashService, IJwtService jwtService, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _hashService = hashService;
            _jwtService = jwtService;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Result<TokenPair>> LoginAsync(string email, string password, string ip, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new Result<TokenPair>(new FieldIsRequiredException(nameof(email)));
            }
            if (string.IsNullOrEmpty(password))
            {
                return new Result<TokenPair>(new FieldIsRequiredException(nameof(password)));
            }
            if (string.IsNullOrEmpty(ip))
            {
                return new Result<TokenPair>(new FieldIsRequiredException(nameof(ip)));
            }

            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

            if (user is null)
            {
                return new Result<TokenPair>(new InvalidCastException());
            }

            if(!_hashService.VerifyHash(password, user.Salt, user.PasswordHash))
            {
                return new Result<TokenPair>(new InvalidCastException());
            }

            var pair = _jwtService.CreateTokenPair(user.Id, ip);

            await _refreshTokenRepository.CreateAsync(pair.RefreshToken, cancellationToken);

            return pair;
        }

        public async Task LogoutAsync(Guid refreshTokenId, string ip, CancellationToken cancellationToken)
        {
            var token = await _refreshTokenRepository.GetByIdAsync(refreshTokenId, cancellationToken);

            if (token is null) 
            {
                return;
            }

            token.RevokeByIp(ip);

            await _refreshTokenRepository.UpdateAsync(token, cancellationToken);
        }

        public async Task<Result<TokenPair>> RefreshPairAsync(string token, string ip, CancellationToken cancellationToken)
        {
            var oldToken = await _refreshTokenRepository.GetByTokenAsync(token, cancellationToken);

            if (oldToken is null || !oldToken.IsActive)
            {
                return new Result<TokenPair>(new TokenNotFoundOrExpiredException());
            }
            
            var user = await _userRepository.GetByIdAsync(oldToken.UserId, cancellationToken);
            if(user is null)
            {
                return new Result<TokenPair>(new TokenNotFoundOrExpiredException());
            }

            var pair = _jwtService.CreateTokenPair(user.Id, ip);
            oldToken.RevokeByToken(pair.RefreshToken);

            await _refreshTokenRepository.UpdateAsync(oldToken, cancellationToken);
            await _refreshTokenRepository.CreateAsync(pair.RefreshToken, cancellationToken);

            return pair;
        }
    }
}
