using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.API.Handlers;

public interface ICrudHandler<TId, TModel> where TModel : class
{
    IEnumerable<TModel> List();
    TModel? Get(TId id);
    Task<TModel?> GetAsync(TId id);
    TId Create(TModel model);
    Task<TId> CreateAsync(TModel model);
    void Update(TId id, TModel model);
    Task UpdateAsync(TId id, TModel model);
    void Delete(TId id);
    Task DeleteAsync(TId id);
}