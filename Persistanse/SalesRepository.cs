using Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistanse
{
    public class SalesRepository : BaseRepository<Sale>, ISaleRepository
    {
        public SalesRepository(IMongoDatabase database) : base(database, "sales")
        {
        }
    }
}
