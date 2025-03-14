using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public sealed record SaleResponse(Guid UserId, string Title, string Description, decimal Price, DateTime ModifiedOn);

}
