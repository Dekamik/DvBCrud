using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DvBCrud.EFCore.API.XMLJSON
{
    [Obsolete("Use CRUDController and specify CRUDAction.Read as the only allowed action")]
    public interface IReadOnlyController<TEntity, TId>
    {
        [HttpGet, Route("{id}")]
        ActionResult<TEntity> Read([FromQuery]TId id);

        [HttpGet]
        ActionResult<IEnumerable<TEntity>> ReadAll();
    }
}
