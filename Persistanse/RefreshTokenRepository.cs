using Domain.Auth;
using MongoDB.Driver;

namespace Persistanse
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IMongoDatabase database) : base(database, "refresh-tokens")
        {

        }

        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await Collection
                .Find(x => x.Token == token)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
 
}