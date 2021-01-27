using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.API.XMLJSON
{
    public abstract class AsyncCRUDController<TEntity, TId, TRepository, TDbContext> : ReadOnlyController<TEntity, TId, TRepository, TDbContext>, IAsyncCRUDController<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TRepository : IRepository<TEntity, TId>
        where TDbContext : DbContext
    {
        public AsyncCRUDController(TRepository repository, ILogger logger) : base(repository, logger)
        {

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TEntity entity)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Create)} {nameof(TEntity)}");

            await Task.Run(() => repository.Create(entity));
            await repository.SaveChangesAsync();

            logger.LogDebug($"{guid}: {nameof(Create)} OK");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRange([FromBody]IEnumerable<TEntity> entities)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(CreateRange)} {entities.Count()} {nameof(TEntity)}");

            await Task.Run(() => repository.CreateRange(entities));
            await repository.SaveChangesAsync();

            logger.LogDebug($"{guid}: {nameof(CreateRange)} OK");
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TEntity entity, [FromQuery] bool createIfNotExists = false)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Update)} {nameof(TEntity)}.Id = {entity}{(createIfNotExists ? ", createIfNotExists = true" : "")}");

            await repository.UpdateAsync(entity, createIfNotExists);
            await repository.SaveChangesAsync();

            logger.LogDebug($"{guid}: {nameof(Update)} OK");
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRange([FromBody]IEnumerable<TEntity> entities, [FromQuery]bool createIfNotExists = false)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(UpdateRange)} {entities.Count()} {nameof(TEntity)}.Id = {string.Join(", ", entities)}{(createIfNotExists ? ", createIfNotExists = true" : "")}");

            await Task.Run(() => repository.UpdateRange(entities, createIfNotExists));
            await repository.SaveChangesAsync();

            logger.LogDebug($"{guid}: {nameof(UpdateRange)} OK");
            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete([FromQuery]TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Delete)} {nameof(TEntity)}.Id = {id}");

            await Task.Run(() => repository.Delete(id));
            await repository.SaveChangesAsync();

            logger.LogDebug($"{guid}: {nameof(Delete)} OK");
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRange([FromBody]IEnumerable<TId> id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(DeleteRange)} {nameof(TEntity)}.Id = {string.Join(", ", id)}");

            await Task.Run(() => repository.DeleteRange(id));
            await repository.SaveChangesAsync();

            logger.LogDebug($"{guid}: {nameof(DeleteRange)} OK");
            return Ok();
        }
    }
}
