﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Requests
{
    public sealed record CreateSaleRequest(Guid UserId, string Title, string Description, decimal Price);
     
}
