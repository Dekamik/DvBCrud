using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Create)} {nameof(TEntity)}");

            repository.Create(entity);
            await repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Create)} OK");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]IEnumerable<TEntity> entities)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Create)} {entities.Count()} {nameof(TEntity)}");

            repository.CreateRange(entities);
            await repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Create)} OK");
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TEntity entity, [FromQuery] bool createIfNotExists = false)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Update)} {nameof(TEntity)}.Id = {entity}{(createIfNotExists ? ", createIfNotExists = true" : "")}");

            await repository.Update(entity, createIfNotExists);
            await repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Update)} OK");
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]IEnumerable<TEntity> entities, [FromQuery]bool createIfNotExists = false)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Update)} {entities.Count()} {nameof(TEntity)}.Id = {string.Join(", ", entities)}{(createIfNotExists ? ", createIfNotExists = true" : "")}");

            repository.UpdateRange(entities, createIfNotExists);
            await repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Update)} OK");
            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete([FromQuery]TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Delete)} {nameof(TEntity)}.Id = {id}");

            repository.Delete(id);
            await repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Delete)} OK");
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]IEnumerable<TId> id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Delete)} {nameof(TEntity)}.Id = {string.Join(", ", id)}");

            repository.DeleteRange(id);
            await repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Delete)} OK");
            return Ok();
        }
    }
}
