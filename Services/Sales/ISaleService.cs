using Domain;
using LanguageExt.Common;

namespace Services.Sales
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken);

        Task<Result<Sale>> CreateAsync(Guid UserId, string Title, string Description, decimal Price, CancellationToken cancellationToken);

    }
}
