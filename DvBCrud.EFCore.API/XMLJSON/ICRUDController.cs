using DvBCrud.EFCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DvBCrud.EFCore.API.XMLJSON
{
    public interface ICRUDController<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        ActionResult<TEntity> Read(TId id);

        ActionResult<IEnumerable<TEntity>> ReadAll();

        IActionResult Create(TEntity entity);

        IActionResult Update(TId id, [FromBody] TEntity entity);

        IActionResult Delete(TId id);
    }
}
