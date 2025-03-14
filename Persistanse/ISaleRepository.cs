using Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistanse
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken);

        Task<Sale> CreateAsync(Sale entity, CancellationToken cancellationToken);

        Task<Sale> UpdateAsync(Sale entity, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
