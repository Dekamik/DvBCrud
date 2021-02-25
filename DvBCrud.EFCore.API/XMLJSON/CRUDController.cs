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
    [ApiController]
    [Route("[controller]")]
    public abstract class CRUDController<TEntity, TId, TRepository, TDbContext> : ControllerBase
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

        [HttpPost]
        public IActionResult Create([FromBody] TEntity entity)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Create)} {typeof(TEntity).Name}");

            if (!crudActions.IsActionAllowed(CRUDAction.Create))
            {
                var message = $"Create forbidden on {typeof(TEntity).Name}";
                logger.LogDebug($"{guid}: {nameof(Read)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

            // Id must NOT be predefined
            if (!entity.Id.Equals(default(TId)))
            {
                string message = $"{typeof(TEntity).Name}.Id must NOT be predefined.";
                logger.LogDebug($"{guid}: {nameof(Create)} BAD REQUEST - {message}");
                return BadRequest(message);
            }

            repository.Create(entity);
            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Create)} OK");
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<TEntity> Read(TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Read)} {typeof(TEntity).Name} {id}");

            if (!crudActions.IsActionAllowed(CRUDAction.Read))
            {
                var message = $"Read forbidden on {typeof(TEntity).Name}";
                logger.LogDebug($"{guid}: {nameof(Read)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

            TEntity entity = repository.Get(id);

            if (entity == null)
            {
                var message = $"{typeof(TEntity).Name} {id} not found.";
                logger.LogDebug($"{guid}: {nameof(Read)} NOT FOUND - {message}");
                return NotFound(message);
            }

            logger.LogDebug($"{guid}: {nameof(Read)} OK");
            return Ok(entity);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TEntity>> ReadAll()
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(ReadAll)} {typeof(TEntity).Name}");

            if (!crudActions.IsActionAllowed(CRUDAction.Read))
            {
                var message = $"Read forbidden on {typeof(TEntity).Name}";
                logger.LogDebug($"{guid}: {nameof(ReadAll)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

            IEnumerable<TEntity> entities = repository.GetAll();

            logger.LogDebug($"{guid}: {nameof(ReadAll)} OK");
            return Ok(entities);
        }

        [HttpPut("{id}")]
        public IActionResult Update(TId id, [FromBody] TEntity entity)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Update)} {typeof(TEntity).Name} {id}");

            if (!crudActions.IsActionAllowed(CRUDAction.Update))
            {
                var message = $"Update forbidden on {typeof(TEntity).Name}";
                logger.LogDebug($"{guid}: {nameof(Read)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

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
                string message = $"{typeof(TEntity).Name} {entity.Id} not found.";
                logger.LogDebug($"{guid}: {nameof(Update)} NOT FOUND - {message}");
                return NotFound(message);
            }

            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Update)} OK");
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Delete)} {typeof(TEntity).Name} {id}");

            if (!crudActions.IsActionAllowed(CRUDAction.Delete))
            {
                var message = $"Delete forbidden on {typeof(TEntity).Name}";
                logger.LogDebug($"{guid}: {nameof(Read)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

            try
            {
                repository.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                string message = $"{typeof(TEntity).Name} {id} not found.";
                logger.LogDebug($"{guid}: {nameof(Delete)} NOT FOUND - {message}");
                return NotFound(message);
            }
            
            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Delete)} OK");
            return Ok();
        }

        protected ObjectResult Forbidden(string message) => StatusCode(403, message);
    }
}
