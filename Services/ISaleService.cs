using Domain;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken);

        Task<Result<Sale>> CreateAsync(Guid UserId, string Title, string Description, decimal Price, CancellationToken cancellationToken);

    }
}
