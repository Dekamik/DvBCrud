using System.Collections.Generic;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.EFCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class CrudController<TEntity, TId, TRepository> : ControllerBase
        where TEntity : BaseEntity<TId>
        where TRepository : IRepository<TEntity, TId>
    {
        protected readonly TRepository Repository;
        protected readonly CrudActionPermissions CrudActions;

        public CrudController(TRepository repository)
        {
            Repository = repository;
            CrudActions = new CrudActionPermissions();
        }

        public CrudController(TRepository repository, params CrudAction[]? allowedActions)
        {
            Repository = repository;
            CrudActions = new CrudActionPermissions(allowedActions);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TEntity entity)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
            {
                var message = $"Create forbidden on {typeof(TEntity).Name}";
                return Forbidden(message);
            }

            // Id must NOT be predefined
            if (entity.Id != null && !entity.Id.Equals(default(TId)))
            {
                return BadRequest($"{typeof(TEntity).Name}.Id must NOT be predefined.");
            }

            Repository.Create(entity);
            Repository.SaveChanges();

            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<TEntity> Read(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                var message = $"Read forbidden on {typeof(TEntity).Name}";
                return Forbidden(message);
            }

            var entity = Repository.Get(id);

            if (entity == null)
            {
                var message = $"{typeof(TEntity).Name} {id} not found.";
                return NotFound(message);
            }

            return Ok(entity);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TEntity>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                var message = $"Read forbidden on {typeof(TEntity).Name}";
                return Forbidden(message);
            }

            var entities = Repository.GetAll();

            return Ok(entities);
        }

        [HttpPut("{id}")]
        public IActionResult Update(TId id, [FromBody] TEntity entity)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Update))
            {
                var message = $"Update forbidden on {typeof(TEntity).Name}";
                return Forbidden(message);
            }

            if (entity.Id == null || entity.Id.Equals(default(TId)))
            {
                return BadRequest($"{nameof(id)} must be defined.");
            }

            try
            {
                Repository.Update(id, entity);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"{typeof(TEntity).Name} {entity.Id} not found.");
            }

            Repository.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Delete))
            {
                var message = $"Delete forbidden on {typeof(TEntity).Name}";
                return Forbidden(message);
            }

            try
            {
                Repository.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                var message = $"{typeof(TEntity).Name} {id} not found.";
                return NotFound(message);
            }
            
            Repository.SaveChanges();

            return Ok();
        }

        protected ObjectResult Forbidden(string message) => StatusCode(403, message);
    }
}
