using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DvBCrud.EFCore.API.JSON
{
    public interface IReadOnlyController<TEntity, TId>
    {
        [HttpGet, Route("{id}")]
        ActionResult<TEntity> Read(TId id);

        [HttpGet]
        ActionResult<IEnumerable<TEntity>> ReadAll();
    }
}
