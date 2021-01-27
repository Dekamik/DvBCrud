using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

            // Id must NOT be predefined
            if (!entity.Id.Equals(default(TId)))
            {
                string message = $"{nameof(TEntity)}.Id must NOT be predefined.";
                logger.LogDebug($"{guid}: {nameof(Create)} BAD REQUEST - {message}");
                return BadRequest(message);
            }

            await Task.Run(() => repository.Create(entity));
            await repository.SaveChangesAsync();

            logger.LogDebug($"{guid}: {nameof(Create)} OK");
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TEntity entity, [FromQuery] bool createIfNotExists = false)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Update)} {nameof(TEntity)} {entity.Id}{(createIfNotExists ? ", createIfNotExists = true" : "")}");

            // Id must be predefined
            if (entity.Id.Equals(default(TId)))
            {
                string message = $"{nameof(TEntity)}.Id must be defined.";
                logger.LogDebug($"{guid}: {nameof(Create)} BAD REQUEST - {message}");
                return BadRequest(message);
            }

            try
            {
                await repository.UpdateAsync(entity, createIfNotExists);
            }
            catch (KeyNotFoundException)
            {
                string message = $"{nameof(TEntity)} {entity.Id} not found.";
                logger.LogDebug($"{guid}: {nameof(Update)} NOT FOUND - {message}");
                return NotFound(message);
            }
            
            await repository.SaveChangesAsync();

            logger.LogDebug($"{guid}: {nameof(Update)} OK");
            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete([FromQuery]TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Delete)} {nameof(TEntity)} {id}");

            await Task.Run(() => repository.Delete(id));
            await repository.SaveChangesAsync();

            logger.LogDebug($"{guid}: {nameof(Delete)} OK");
            return Ok();
        }
    }
}
