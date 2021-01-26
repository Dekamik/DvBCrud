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
            logger.LogDebug($"{nameof(Create)} {nameof(TEntity)} ({guid})");

            repository.Create(entity);
            await repository.SaveChanges();

            logger.LogDebug($"{nameof(Create)} OK ({guid})");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]IEnumerable<TEntity> entities)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{nameof(Create)} {entities.Count()} {nameof(TEntity)} ({guid})");

            repository.CreateRange(entities);
            await repository.SaveChanges();

            logger.LogDebug($"{nameof(Create)} OK ({guid})");
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TEntity entity, [FromQuery] bool createIfNotExists = false)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{nameof(Update)} {nameof(TEntity)}.Id = {entity}{(createIfNotExists ? ", createIfNotExists = true" : "")} ({guid})");

            await repository.Update(entity, createIfNotExists);
            await repository.SaveChanges();

            logger.LogDebug($"{nameof(Update)} OK ({guid})");
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]IEnumerable<TEntity> entities, [FromQuery]bool createIfNotExists = false)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{nameof(Update)} {entities.Count()} {nameof(TEntity)}.Id = {string.Join(", ", entities)}{(createIfNotExists ? ", createIfNotExists = true" : "")} ({guid})");

            repository.UpdateRange(entities, createIfNotExists);
            await repository.SaveChanges();

            logger.LogDebug($"{nameof(Update)} OK ({guid})");
            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete([FromQuery]TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{nameof(Delete)} {nameof(TEntity)}.Id = {id} ({guid})");

            repository.Delete(id);
            await repository.SaveChanges();

            logger.LogDebug($"{nameof(Delete)} OK ({guid})");
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]IEnumerable<TId> id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{nameof(Delete)} {nameof(TEntity)}.Id = {string.Join(", ", id)} ({guid})");

            repository.DeleteRange(id);
            await repository.SaveChanges();

            logger.LogDebug($"{nameof(Delete)} OK ({guid})");
            return Ok();
        }
    }
}
