using DvBCrud.EFCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.API.XMLJSON
{
    public interface ICRUDController<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        [HttpPost]
        IActionResult Create([FromBody] TEntity entity);

        [HttpPost]
        IActionResult CreateRange([FromBody]IEnumerable<TEntity> entities);

        [HttpPut]
        IActionResult Update([FromBody] TEntity entity, [FromQuery] bool createIfNotExists = false);

        [HttpPut]
        IActionResult UpdateRange([FromBody]IEnumerable<TEntity> entities, [FromQuery] bool createIfNotExists = false);

        [HttpDelete, Route("{id}")]
        IActionResult Delete([FromQuery]TId id);

        [HttpDelete]
        IActionResult DeleteRange([FromBody]IEnumerable<TId> ids);
    }
}
