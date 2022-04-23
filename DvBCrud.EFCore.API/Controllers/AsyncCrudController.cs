using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncCrudController<TEntity, TId, TRepository> : ControllerBase
        where TEntity : BaseEntity<TId>
        where TRepository : IRepository<TEntity, TId>
    {
        protected readonly TRepository Repository;
        protected readonly ILogger Logger;
        protected readonly CrudActionPermissions CrudActions;

        public AsyncCrudController(TRepository repository, ILogger logger)
        {
            Repository = repository;
            Logger = logger;
            CrudActions = new CrudActionPermissions();
        }

        public AsyncCrudController(TRepository repository, ILogger logger, params CrudAction[]? allowedActions)
        {
            Repository = repository;
            Logger = logger;
            CrudActions = new CrudActionPermissions(allowedActions);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TEntity entity)
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

            await Task.Run(() => Repository.Create(entity));
            await Repository.SaveChangesAsync();

            Logger.LogDebug($"{guid}: {nameof(Create)} OK");
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Read(TId id)
        {
            var guid = Guid.NewGuid();
            Logger.LogDebug($"{guid}: {nameof(Read)} {typeof(TEntity).Name} {id}");

            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                var message = $"Read forbidden on {typeof(TEntity).Name}";
                Logger.LogDebug($"{guid}: {nameof(Read)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

            TEntity entity = await Repository.GetAsync(id);

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
        public async Task<ActionResult<IEnumerable<TEntity>>> ReadAll()
        {
            var guid = Guid.NewGuid();
            Logger.LogDebug($"{guid}: {nameof(ReadAll)} {typeof(TEntity).Name}");

            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                var message = $"Read forbidden on {typeof(TEntity).Name}";
                Logger.LogDebug($"{guid}: {nameof(Read)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

            IEnumerable<TEntity> entities = await Task.Run(() => Repository.GetAll());

            Logger.LogDebug($"{guid}: {nameof(ReadAll)} OK");
            return Ok(entities);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(TId id, [FromBody] TEntity entity)
        {
            var guid = Guid.NewGuid();
            Logger.LogDebug($"{guid}: {nameof(Update)} {typeof(TEntity).Name} {id}");

            if (!CrudActions.IsActionAllowed(CrudAction.Update))
            {
                var message = $"Update forbidden on {typeof(TEntity).Name}";
                Logger.LogDebug($"{guid}: {nameof(Read)} FORBIDDEN - {message}");
                return Forbidden(message);
            }

            // Id must be predefined
            if (entity.Id.Equals(default(TId)))
            {
                string message = $"{nameof(id)} must be defined.";
                Logger.LogDebug($"{guid}: {nameof(Create)} BAD REQUEST - {message}");
                return BadRequest(message);
            }

            try
            {
                await Repository.UpdateAsync(id, entity);
            }
            catch (KeyNotFoundException)
            {
                string message = $"{typeof(TEntity).Name} {entity.Id} not found.";
                Logger.LogDebug($"{guid}: {nameof(Update)} NOT FOUND - {message}");
                return NotFound(message);
            }
            
            await Repository.SaveChangesAsync();

            Logger.LogDebug($"{guid}: {nameof(Update)} OK");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(TId id)
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
                await Task.Run(() => Repository.Delete(id));
            }
            catch (KeyNotFoundException)
            {
                string message = $"{typeof(TEntity).Name} {id} not found.";
                Logger.LogDebug($"{guid}: {nameof(Delete)} NOT FOUND - {message}");
                return NotFound(message);
            }
            
            await Repository.SaveChangesAsync();

            Logger.LogDebug($"{guid}: {nameof(Delete)} OK");
            return Ok();
        }

        protected ObjectResult Forbidden(string message) => StatusCode(403, message);
    }
}
