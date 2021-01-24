using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.API.JSON
{
    public interface IReadOnlyController<TEntity, TId>
    {
        [HttpGet, Route("{id}")]
        Task<ActionResult<TEntity>> Read(TId id);

        [HttpGet]
        ActionResult<IEnumerable<TEntity>> ReadAll();
    }
}
