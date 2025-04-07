using Domain.Auth;

namespace Persistanse
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken);

        Task<RefreshToken> CreateAsync(RefreshToken token, CancellationToken cancellationToken);

        Task<RefreshToken> UpdateAsync(RefreshToken token, CancellationToken cancellationToken);

    }
}
