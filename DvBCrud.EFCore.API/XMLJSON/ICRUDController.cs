using DvBCrud.EFCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.EFCore.API.XMLJSON
{
    public interface ICRUDController<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        [HttpPost]
        IActionResult Create([FromBody] TEntity entity);

        [HttpPut]
        IActionResult Update([FromQuery] TId id, [FromBody] TEntity entity);

        [HttpDelete, Route("{id}")]
        IActionResult Delete([FromQuery]TId id);
    }
}
