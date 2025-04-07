using Domain;

namespace Persistanse
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task<User> CreateAsync(User user, CancellationToken cancellationToken);
    }
}
