using Domain;
using MongoDB.Driver;

namespace Persistanse
{
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        public SaleRepository(IMongoDatabase database) : base(database, "sales")
        {
        }
    }
}
