using DvBCrud.EFCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.API.JSON
{
    public interface ICRUDController<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        [HttpPost]
        Task<IActionResult> Create([FromBody] TEntity entity);

        [HttpPost]
        Task<IActionResult> Create([FromBody]IEnumerable<TEntity> entities);

        [HttpPut]
        Task<IActionResult> Update([FromBody] TEntity entity, [FromQuery] bool createIfNotExists = false);

        [HttpPut]
        Task<IActionResult> Update([FromBody]IEnumerable<TEntity> entities, [FromQuery] bool createIfNotExists = false);

        [HttpDelete, Route("{id}")]
        Task<IActionResult> Delete([FromQuery]TId id);

        [HttpDelete]
        Task<IActionResult> Delete([FromBody]IEnumerable<TId> ids);
    }
}
