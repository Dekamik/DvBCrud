using DvBCrud.EFCore.API.CRUDActions;
using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DvBCrud.EFCore.API.XMLJSON
{
    public abstract class CRUDController<TEntity, TId, TRepository, TDbContext> : ControllerBase, ICRUDController<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TRepository : IRepository<TEntity, TId>
        where TDbContext : DbContext
    {
        protected readonly TRepository repository;
        protected readonly ILogger logger;
        protected readonly CRUDActionPermissions crudActions;

        public CRUDController(TRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
            this.crudActions = new CRUDActionPermissions();
        }

        public CRUDController(TRepository repository, ILogger logger, params CRUDAction[] allowedActions)
        {
            this.repository = repository;
            this.logger = logger;
            this.crudActions = new CRUDActionPermissions(allowedActions);
        }

        public IActionResult Create([FromBody] TEntity entity)
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

            repository.Create(entity);
            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Create)} OK");
            return Ok();
        }

        public ActionResult<TEntity> Read([FromQuery] TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Read)} {nameof(TEntity)} {id}");

            TEntity entity = repository.Get(id);

            if (entity == null)
            {
                var message = $"{nameof(TEntity)} {id} not found.";
                logger.LogDebug($"{guid}: {nameof(Read)} NOT FOUND - {message}");
                return NotFound(message);
            }

            logger.LogDebug($"{guid}: {nameof(Read)} OK");
            return Ok(entity);
        }

        public ActionResult<IEnumerable<TEntity>> ReadAll()
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(ReadAll)} {nameof(TEntity)}");

            IEnumerable<TEntity> entities = repository.GetAll();

            logger.LogDebug($"{guid}: {nameof(ReadAll)} OK");
            return Ok(entities);
        }

        public IActionResult Update([FromQuery] TId id, [FromBody] TEntity entity)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Update)} {nameof(TEntity)} {id}");

            if (id.Equals(default(TId)))
            {
                string message = $"{nameof(id)} must be defined.";
                logger.LogDebug($"{guid}: {nameof(Update)} BAD REQUEST - {message}");
                return BadRequest(message);
            }

            try
            {
                repository.Update(id, entity);
            }
            catch (KeyNotFoundException)
            {
                string message = $"{nameof(TEntity)} {entity.Id} not found.";
                logger.LogDebug($"{guid}: {nameof(Update)} NOT FOUND - {message}");
                return NotFound(message);
            }

            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Update)} OK");
            return Ok();
        }

        public IActionResult Delete([FromQuery]TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Delete)} {nameof(TEntity)} {id}");

            try
            {
                repository.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                string message = $"{nameof(TEntity)} {id} not found.";
                logger.LogDebug($"{guid}: {nameof(Delete)} NOT FOUND - {message}");
                return NotFound(message);
            }
            
            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Delete)} OK");
            return Ok();
        }
    }
}
