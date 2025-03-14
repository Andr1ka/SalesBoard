using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class FieldIsRequiredException : Exception
    {
        public FieldIsRequiredException(string fieldName) : base($"{fieldName} is required")
        {
        }
    }
}
