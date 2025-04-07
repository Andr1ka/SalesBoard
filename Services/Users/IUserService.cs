using Domain;
using LanguageExt.Common;

namespace Services.Users
{
    public interface IUserService
    {
        Task<Result<User>> CreateAsync(string firstName, string lastName, string email, string password, CancellationToken cancellationToken);
    }
}
