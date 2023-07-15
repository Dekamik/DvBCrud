using DvBCrud.Shared;
using DvBCrud.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
// ReSharper disable MemberCanBePrivate.Global

namespace DvBCrud.Mongo;

public abstract class Repository<TEntity, TMapper, TModel, TFilter> : IRepository<ObjectId, TModel, TFilter>
    where TEntity : class, IEntity<ObjectId>
    where TMapper : BaseMapper<TEntity, TModel, TFilter>
{
    protected readonly IMongoCollection<TEntity> Collection;
    protected readonly TMapper Mapper;

    protected Repository(IMongoCollection<TEntity> collection, TMapper mapper)
    {
        Collection = collection;
        Mapper = mapper;
    }

    public IEnumerable<TModel> List(TFilter filter)
    {
        return Mapper.FilterAndSort(Collection.AsQueryable(), filter)
            .Select(Mapper.ToModel);
    }

    public TModel Get(ObjectId id)
    {
        var filter = Builders<TEntity>.Filter.Eq(nameof(IEntity<ObjectId>.Id), id);
        var entity = Collection.Find(filter).SingleOrDefault();
        return Mapper.ToModel(entity);
    }

    public async Task<TModel> GetAsync(ObjectId id)
    {
        var filter = Builders<TEntity>.Filter.Eq(nameof(IEntity<ObjectId>.Id), id);
        var entity = await (await Collection.FindAsync(filter)).SingleOrDefaultAsync();
        return Mapper.ToModel(entity);
    }

    public ObjectId Create(TModel model)
    {
        var entity = Mapper.ToEntity(model);
        Collection.InsertOne(entity);
        return entity.Id;
    }

    public async Task<ObjectId> CreateAsync(TModel model)
    {
        var entity = Mapper.ToEntity(model);
        await Collection.InsertOneAsync(entity);
        return entity.Id;
    }

    public void Update(ObjectId id, TModel model)
    {
        var filter = Builders<TEntity>.Filter.Eq(nameof(IEntity<ObjectId>.Id), id);
        var existingEntity = Collection.Find(filter).SingleOrDefault();

        var entity = Mapper.ToEntity(model);
        Mapper.UpdateEntity(existingEntity, entity);
        Collection.ReplaceOne(filter, entity);
    }

    public async Task UpdateAsync(ObjectId id, TModel model)
    {
        var filter = Builders<TEntity>.Filter.Eq(nameof(IEntity<ObjectId>.Id), id);
        var existingEntity = await (await Collection.FindAsync(filter)).SingleOrDefaultAsync();

        var entity = Mapper.ToEntity(model);
        Mapper.UpdateEntity(existingEntity, entity);
        await Collection.ReplaceOneAsync(filter, entity);
    }

    public void Delete(ObjectId id)
    {
        var filter = Builders<TEntity>.Filter.Eq(nameof(IEntity<ObjectId>.Id), id);
        Collection.DeleteOne(filter);
    }

    public Task DeleteAsync(ObjectId id)
    {
        var filter = Builders<TEntity>.Filter.Eq(nameof(IEntity<ObjectId>.Id), id);
        return Collection.DeleteOneAsync(filter);
    }
}