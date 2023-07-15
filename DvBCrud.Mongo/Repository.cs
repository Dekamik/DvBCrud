using DvBCrud.Shared;
using DvBCrud.Shared.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DvBCrud.Mongo;

public abstract class Repository<TEntity, TMapper, TModel> : IRepository<ObjectId, TModel>
    where TEntity : class, IEntity<ObjectId>
    where TMapper : IMapper<TEntity, TModel>
{
    private readonly IMongoCollection<TEntity> _collection;
    private readonly TMapper _mapper;

    protected Repository(IMongoCollection<TEntity> collection, TMapper mapper)
    {
        _collection = collection;
        _mapper = mapper;
    }

    public IEnumerable<TModel> List()
    {
        throw new NotImplementedException();
    }

    public TModel Get(ObjectId id)
    {
        var filter = Builders<TEntity>.Filter.Eq(nameof(IEntity<ObjectId>.Id), id);
        var entity = _collection.Find(filter).SingleOrDefault();
        return _mapper.ToModel(entity);
    }

    public async Task<TModel> GetAsync(ObjectId id)
    {
        var filter = Builders<TEntity>.Filter.Eq(nameof(IEntity<ObjectId>.Id), id);
        var entity = await (await _collection.FindAsync(filter)).SingleOrDefaultAsync();
        return _mapper.ToModel(entity);
    }

    public ObjectId Create(TModel model)
    {
        var entity = _mapper.ToEntity(model);
        _collection.InsertOne(entity);
        return entity.Id;
    }

    public async Task<ObjectId> CreateAsync(TModel model)
    {
        var entity = _mapper.ToEntity(model);
        await _collection.InsertOneAsync(entity);
        return entity.Id;
    }

    public void Update(ObjectId id, TModel model)
    {
        var filter = Builders<TEntity>.Filter.Eq(nameof(IEntity<ObjectId>.Id), id);
        var existingEntity = _collection.Find(filter).SingleOrDefault();

        var entity = _mapper.ToEntity(model);
        _mapper.UpdateEntity(existingEntity, entity);
        _collection.ReplaceOne(filter, entity);
    }

    public async Task UpdateAsync(ObjectId id, TModel model)
    {
        var filter = Builders<TEntity>.Filter.Eq(nameof(IEntity<ObjectId>.Id), id);
        var existingEntity = await (await _collection.FindAsync(filter)).SingleOrDefaultAsync();

        var entity = _mapper.ToEntity(model);
        _mapper.UpdateEntity(existingEntity, entity);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public void Delete(ObjectId id)
    {
        var filter = Builders<TEntity>.Filter.Eq(nameof(IEntity<ObjectId>.Id), id);
        _collection.DeleteOne(filter);
    }

    public Task DeleteAsync(ObjectId id)
    {
        var filter = Builders<TEntity>.Filter.Eq(nameof(IEntity<ObjectId>.Id), id);
        return _collection.DeleteOneAsync(filter);
    }

    public bool Exists(ObjectId id)
    {
        // Not implemented - possibly slated for deletion
        throw new NotImplementedException();
    }
}