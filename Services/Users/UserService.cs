using Domain;
using LanguageExt.Common;
using Persistanse;
using Services.Auth;

namespace Services.Users
{
    public class UserService : IUserService
    {
        private readonly IHashService _hashService;
        private readonly IUserRepository _userRepository;

        public UserService(IHashService hashService, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _hashService  = hashService;
        }

        public async Task<Result<User>> CreateAsync(string firstName, string lastName, string email, string password, CancellationToken cancellationToken)
        {

            var salt = _hashService.GenerateSalt();
            var hash = _hashService.CalculateHash(password,salt);


            var user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Salt = salt,
                PasswordHash = hash
            };

            var result = await _userRepository.CreateAsync(user, cancellationToken);

            return new Result<User>(result);
        }
    }
}
