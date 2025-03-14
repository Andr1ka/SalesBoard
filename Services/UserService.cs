using Domain;
using LanguageExt.Common;
using Persistanse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> CreateAsync(string firstName, string lastName, string email, string password, CancellationToken cancellationToken)
        {

            ///refactor this
            var salt = "123123";
            var hash = password + salt;


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
