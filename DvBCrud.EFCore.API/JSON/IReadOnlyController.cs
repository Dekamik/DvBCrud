using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.API.JSON
{
    public interface IReadOnlyController<TEntity, TId>
    {
        [HttpGet, Route("{id}")]
        Task<ActionResult<TEntity>> Read([FromQuery]TId id);

        [HttpGet]
        ActionResult<IEnumerable<TEntity>> Read([FromBody]IEnumerable<TId> ids);

        [HttpGet]
        ActionResult<IEnumerable<TEntity>> ReadAll();
    }
}
