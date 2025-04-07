using Domain.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Auth
{
    public class JwtService : IJwtService
    { 
        private readonly IHashService _hashService;
        private readonly AuthOptions _options;

       public JwtService(IHashService hashService, IOptions<AuthOptions> options)
        {
            _hashService = hashService;
            _options = options.Value;
        }
        public TokenPair CreateTokenPair(Guid userId, string ip)
        {
            var refreshToken = CreateRefreshToken(userId, ip);
            var acsessToken = CreateAccessToken(userId, ip, refreshToken.Id);
            return new TokenPair(acsessToken, refreshToken);
        }

        private RefreshToken CreateRefreshToken(Guid userId, string ip)
        {
            var expires = DateTime.UtcNow.AddDays(_options.RefreshTokenTimeToLiveInDays);
            var token = _hashService.GenerateToken();
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                CreatedByIp = ip,
                Token = token,
                Expires = expires
            };
            return refreshToken;
        }

        private string CreateAccessToken(Guid userId, string ip, Guid RefreshTokenId)
        {
            var identity = new ClaimsIdentity([
                new(ApplicationClaims.UserId, userId.ToString()),
                new(ApplicationClaims.RefreshTokenId, userId.ToString()),
                ]);

            var secret = Encoding.UTF8.GetBytes(_options.JwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = _options.JwtIssurer,
                Audience = _options.JwtAudience,
                Expires = DateTime.UtcNow.AddMinutes(_options.AccessTokenTimeToLiveInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
