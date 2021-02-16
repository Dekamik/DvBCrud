using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.API.XMLJSON
{
    [Obsolete("Use AsyncCRUDController and specify CRUDAction.Read as the only allowed action")]
    public interface IAsyncReadOnlyController<TEntity, TId>
    {
        [HttpGet, Route("{id}")]
        Task<ActionResult<TEntity>> Read([FromQuery] TId id);

        [HttpGet]
        Task<ActionResult<IEnumerable<TEntity>>> ReadAll();
    }
}
