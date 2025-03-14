using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class InvalidFieldValue : Exception
    {
        public InvalidFieldValue(string fieldName) : base($"{fieldName} has invalid value")
        {
        }
    }
}
