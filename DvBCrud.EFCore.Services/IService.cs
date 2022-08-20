using DvBCrud.EFCore.Services.Models;

namespace DvBCrud.EFCore.Services;

public interface IService<TId, TModel> where TModel : BaseModel
{
    IEnumerable<TModel> GetAll();
    TModel? Get(TId id);
    Task<TModel?> GetAsync(TId id);
    TId Create(TModel model);
    Task<TId> CreateAsync(TModel model);
    void Update(TId id, TModel model);
    Task UpdateAsync(TId id, TModel model);
    void Delete(TId id);
    Task DeleteAsync(TId id);
}