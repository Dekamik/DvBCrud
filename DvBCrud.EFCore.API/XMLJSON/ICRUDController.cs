using DvBCrud.EFCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DvBCrud.EFCore.API.XMLJSON
{
    public interface ICRUDController<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        [HttpGet, Route("{id}")]
        ActionResult<TEntity> Read([FromQuery] TId id);

        [HttpGet]
        ActionResult<IEnumerable<TEntity>> ReadAll();

        [HttpPost]
        IActionResult Create([FromBody] TEntity entity);

        [HttpPut, Route("{id}")]
        IActionResult Update([FromQuery] TId id, [FromBody] TEntity entity);

        [HttpDelete, Route("{id}")]
        IActionResult Delete([FromQuery]TId id);
    }
}
