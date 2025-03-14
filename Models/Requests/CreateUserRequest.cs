using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Requests
{
    public sealed record CreateUserRequest(string FirstName, string LastName, string Email, string Password);
   
   
}
