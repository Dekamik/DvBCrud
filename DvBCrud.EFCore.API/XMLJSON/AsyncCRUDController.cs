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
    public abstract class AsyncCRUDController<TEntity, TId, TRepository, TDbContext> : ControllerBase, IAsyncCRUDController<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TRepository : IRepository<TEntity, TId>
        where TDbContext : DbContext
    {
        protected readonly TRepository repository;
        protected readonly ILogger logger;

        public AsyncCRUDController(TRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

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

        public async Task<ActionResult<TEntity>> Read([FromQuery] TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Read)} {nameof(TEntity)} {id}");

            TEntity entity = await repository.GetAsync(id);

            if (entity == null)
            {
                var message = $"{nameof(TEntity)} {id} not found.";
                logger.LogDebug($"{guid}: {nameof(Read)} NOT FOUND - {message}");
                return NotFound(message);
            }

            logger.LogDebug($"{guid}: {nameof(Read)} OK");
            return Ok(entity);
        }

        public async Task<ActionResult<IEnumerable<TEntity>>> ReadAll()
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(ReadAll)} {nameof(TEntity)}");

            IEnumerable<TEntity> entities = await Task.Run(() => repository.GetAll());

            logger.LogDebug($"{guid}: {nameof(ReadAll)} OK");
            return Ok(entities);
        }

        public async Task<IActionResult> Update([FromQuery]TId id, [FromBody] TEntity entity)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Update)} {nameof(TEntity)} {id}");

            // Id must be predefined
            if (entity.Id.Equals(default(TId)))
            {
                string message = $"{nameof(id)} must be defined.";
                logger.LogDebug($"{guid}: {nameof(Create)} BAD REQUEST - {message}");
                return BadRequest(message);
            }

            try
            {
                await repository.UpdateAsync(id, entity);
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

        public async Task<IActionResult> Delete([FromQuery]TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Delete)} {nameof(TEntity)} {id}");

            try
            {
                await Task.Run(() => repository.Delete(id));
            }
            catch (KeyNotFoundException)
            {
                string message = $"{nameof(TEntity)} {id} not found.";
                logger.LogDebug($"{guid}: {nameof(Delete)} NOT FOUND - {message}");
                return NotFound(message);
            }
            
            await repository.SaveChangesAsync();

            logger.LogDebug($"{guid}: {nameof(Delete)} OK");
            return Ok();
        }
    }
}
