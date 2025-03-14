using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MongoDB.Driver;

namespace Persistanse
{
    public abstract class BaseRepository<TEntity> where TEntity : PersistableEntity
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName;

        protected BaseRepository(IMongoDatabase database, string collectionName)
        {
            _database = database;   
            _collectionName = collectionName;
        }

        protected IMongoCollection<TEntity> Collection => _database.GetCollection<TEntity>(_collectionName);

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
           return await Collection
                .Find(x => true)
                .ToListAsync(cancellationToken);
        }

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            entity.SetCreatedOn(DateTime.UtcNow);

            var options = new InsertOneOptions();
            await Collection.InsertOneAsync(entity, options, cancellationToken);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            entity.SetModifiedOn(DateTime.UtcNow);

            await Collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);

            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {

            await Collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
        }

    }
}
