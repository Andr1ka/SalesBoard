using Domain.Auth;
using LanguageExt.Common;

namespace Services.Auth
{
    public interface IAuthService
    {
        Task<Result<TokenPair>> LoginAsync(string email, string password, string ip, CancellationToken cancellationToken);

        Task LogoutAsync(Guid refreshTokenId, string ip, CancellationToken cancellationToken);

        Task<Result<TokenPair>> RefreshPairAsync(string token, string ip, CancellationToken cancellationToken);
    }
}
