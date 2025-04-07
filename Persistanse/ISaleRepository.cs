using Domain;

namespace Persistanse
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken);

        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken);

        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
