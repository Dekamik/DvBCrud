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
    public abstract class AsyncReadOnlyController<TEntity, TId, TRepository, TDbContext> : ControllerBase, IAsyncReadOnlyController<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TRepository : IReadOnlyRepository<TEntity, TId>
        where TDbContext : DbContext
    {
        internal readonly TRepository repository;
        internal readonly ILogger logger;

        public AsyncReadOnlyController(TRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<TEntity>> Read([FromQuery] TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Read)} {nameof(TEntity)}.Id = {id}");

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> ReadAll()
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(ReadAll)} {nameof(TEntity)}");

            IEnumerable<TEntity> entities = await Task.Run(() => repository.GetAll());

            logger.LogDebug($"{guid}: {nameof(ReadAll)} OK");
            return Ok(entities);
        }
    }
}
