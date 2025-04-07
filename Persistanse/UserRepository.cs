using Domain;
using MongoDB.Driver;

namespace Persistanse
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoDatabase database) : base(database, "users")
        {
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await Collection.Find(x => x.Email == email).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
