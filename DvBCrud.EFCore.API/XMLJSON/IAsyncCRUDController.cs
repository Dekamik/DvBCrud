using DvBCrud.EFCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.API.XMLJSON
{
    public interface IAsyncCRUDController<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        [HttpPost]
        Task<IActionResult> Create([FromBody] TEntity entity);

        [HttpPut]
        Task<IActionResult> Update([FromQuery] TId id, [FromBody] TEntity entity);

        [HttpDelete, Route("{id}")]
        Task<IActionResult> Delete([FromQuery]TId id);
    }
}
