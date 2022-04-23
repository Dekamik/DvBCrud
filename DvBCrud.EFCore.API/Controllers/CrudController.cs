using System;
using System.Collections.Generic;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class CrudController<TEntity, TId, TRepository> : ControllerBase
        where TEntity : BaseEntity<TId>
        where TRepository : IRepository<TEntity, TId>
    {
        protected readonly TRepository Repository;
        protected readonly ILogger Logger;
        protected readonly CrudActionPermissions CrudActions;

        public CrudController(TRepository repository, ILogger logger)
        {
            Repository = repository;
            Logger = logger;
            CrudActions = new CrudActionPermissions();
        }

        public CrudController(TRepository repository, ILogger logger, params CrudAction[]? allowedActions)
        {
            Repository = repository;
            Logger = logger;
            CrudActions = new CrudActionPermissions(allowedActions);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TEntity entity)
        {
            var guid = Guid.NewGuid();
            Logger.LogDebug($"{guid}: {nameof(Create)} {typeof(TEntity).Name}");

            if (!CrudActions.IsActionAllowed(CrudAction.Create))
            {
                var message = $"Create forbidden on {typeof(TEntity).Name}";
                Logger.LogDebug($"{guid}: {nameof(Read)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

            // Id must NOT be predefined
            if (!entity.Id.Equals(default(TId)))
            {
                string message = $"{typeof(TEntity).Name}.Id must NOT be predefined.";
                Logger.LogDebug($"{guid}: {nameof(Create)} BAD REQUEST - {message}");
                return BadRequest(message);
            }

            Repository.Create(entity);
            Repository.SaveChanges();

            Logger.LogDebug($"{guid}: {nameof(Create)} OK");
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<TEntity> Read(TId id)
        {
            var guid = Guid.NewGuid();
            Logger.LogDebug($"{guid}: {nameof(Read)} {typeof(TEntity).Name} {id}");

            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                var message = $"Read forbidden on {typeof(TEntity).Name}";
                Logger.LogDebug($"{guid}: {nameof(Read)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

            TEntity entity = Repository.Get(id);

            if (entity == null)
            {
                var message = $"{typeof(TEntity).Name} {id} not found.";
                Logger.LogDebug($"{guid}: {nameof(Read)} NOT FOUND - {message}");
                return NotFound(message);
            }

            Logger.LogDebug($"{guid}: {nameof(Read)} OK");
            return Ok(entity);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TEntity>> ReadAll()
        {
            var guid = Guid.NewGuid();
            Logger.LogDebug($"{guid}: {nameof(ReadAll)} {typeof(TEntity).Name}");

            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                var message = $"Read forbidden on {typeof(TEntity).Name}";
                Logger.LogDebug($"{guid}: {nameof(ReadAll)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

            IEnumerable<TEntity> entities = Repository.GetAll();

            Logger.LogDebug($"{guid}: {nameof(ReadAll)} OK");
            return Ok(entities);
        }

        [HttpPut("{id}")]
        public IActionResult Update(TId id, [FromBody] TEntity entity)
        {
            var guid = Guid.NewGuid();
            Logger.LogDebug($"{guid}: {nameof(Update)} {typeof(TEntity).Name} {id}");

            if (!CrudActions.IsActionAllowed(CrudAction.Update))
            {
                var message = $"Update forbidden on {typeof(TEntity).Name}";
                Logger.LogDebug($"{guid}: {nameof(Read)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

            if (entity.Id.Equals(default(TId)))
            {
                string message = $"{nameof(id)} must be defined.";
                Logger.LogDebug($"{guid}: {nameof(Update)} BAD REQUEST - {message}");
                return BadRequest(message);
            }

            try
            {
                Repository.Update(id, entity);
            }
            catch (KeyNotFoundException)
            {
                string message = $"{typeof(TEntity).Name} {entity.Id} not found.";
                Logger.LogDebug($"{guid}: {nameof(Update)} NOT FOUND - {message}");
                return NotFound(message);
            }

            Repository.SaveChanges();

            Logger.LogDebug($"{guid}: {nameof(Update)} OK");
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(TId id)
        {
            var guid = Guid.NewGuid();
            Logger.LogDebug($"{guid}: {nameof(Delete)} {typeof(TEntity).Name} {id}");

            if (!CrudActions.IsActionAllowed(CrudAction.Delete))
            {
                var message = $"Delete forbidden on {typeof(TEntity).Name}";
                Logger.LogDebug($"{guid}: {nameof(Read)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

            try
            {
                Repository.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                string message = $"{typeof(TEntity).Name} {id} not found.";
                Logger.LogDebug($"{guid}: {nameof(Delete)} NOT FOUND - {message}");
                return NotFound(message);
            }
            
            Repository.SaveChanges();

            Logger.LogDebug($"{guid}: {nameof(Delete)} OK");
            return Ok();
        }

        protected ObjectResult Forbidden(string message) => StatusCode(403, message);
    }
}
