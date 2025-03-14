using Domain;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        Task<Result<User>> CreateAsync(string firstName, string lastName, string email, string password, CancellationToken cancellationToken);
    }
}
