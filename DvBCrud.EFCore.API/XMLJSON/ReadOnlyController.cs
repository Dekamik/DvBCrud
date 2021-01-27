using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DvBCrud.EFCore.API.XMLJSON
{
    public abstract class ReadOnlyController<TEntity, TId, TRepository, TDbContext> : ControllerBase, IReadOnlyController<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TRepository : IReadOnlyRepository<TEntity, TId>
        where TDbContext : DbContext
    {
        internal readonly TRepository repository;
        internal readonly ILogger logger;

        public ReadOnlyController(TRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet, Route("{id}")]
        public ActionResult<TEntity> Read([FromQuery]TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Read)} {nameof(TEntity)}.Id = {id}");

            TEntity entity = repository.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            logger.LogDebug($"{guid}: {nameof(Read)} OK");
            return Ok(entity);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TEntity>> ReadAll()
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(ReadAll)} {nameof(TEntity)}");

            IEnumerable<TEntity> entities = repository.GetAll();

            logger.LogDebug($"{guid}: {nameof(ReadAll)} OK");
            return Ok(entities);
        }
    }
}
