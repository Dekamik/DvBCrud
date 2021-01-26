using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.API.JSON
{
    public abstract class CRUDController<TEntity, TId, TRepository, TDbContext> : ReadOnlyController<TEntity, TId, TRepository, TDbContext>, ICRUDController<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TRepository : IRepository<TEntity, TId>
        where TDbContext : DbContext
    {
        public CRUDController(TRepository repository, ILogger logger) : base(repository, logger)
        {

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TEntity entity)
        {
            logger.LogTrace($"{nameof(Create)} request recieved for a {nameof(TEntity)}");

            repository.Create(entity);
            await repository.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]IEnumerable<TEntity> entities)
        {
            logger.LogTrace($"{nameof(Create)} request recieved for {entities.Count()} {nameof(TEntity)}");

            repository.CreateRange(entities);
            await repository.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TEntity entity, [FromQuery] bool createIfNotExists = false)
        {
            logger.LogTrace($"{nameof(Update)} request recieved for a {nameof(TEntity)}.Id = {entity}{(createIfNotExists ? ", createIfNotExists = true" : "")}");

            await repository.Update(entity, createIfNotExists);
            await repository.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]IEnumerable<TEntity> entities, [FromQuery]bool createIfNotExists = false)
        {
            logger.LogTrace($"{nameof(Update)} request recieved for {entities.Count()} {nameof(TEntity)}.Id = {string.Join(", ", entities)}{(createIfNotExists ? ", createIfNotExists = true" : "")}");

            repository.UpdateRange(entities, createIfNotExists);
            await repository.SaveChanges();

            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete([FromQuery]TId id)
        {
            logger.LogTrace($"{nameof(Delete)} request recieved for {nameof(TEntity)}.Id = {id}");

            repository.Delete(id);
            await repository.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]IEnumerable<TId> id)
        {
            logger.LogTrace($"{nameof(Delete)} request recieved for {nameof(TEntity)}.Id = {string.Join(", ", id)}");

            repository.DeleteRange(id);
            await repository.SaveChanges();

            return Ok();
        }
    }
}
