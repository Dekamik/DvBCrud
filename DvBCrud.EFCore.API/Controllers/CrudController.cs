using System;
using System.Collections.Generic;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.Services;
using DvBCrud.EFCore.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.EFCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class CrudController<TId, TModel, TService> : ControllerBase
        where TModel : BaseModel
        where TService : IService<TId, TModel>
    {
        protected readonly TService Service;
        protected readonly CrudActionPermissions CrudActions;

        public CrudController(TService service)
        {
            Service = service;
            CrudActions = new CrudActionPermissions();
        }

        public CrudController(TService service, params CrudAction[]? allowedActions)
        {
            Service = service;
            CrudActions = new CrudActionPermissions(allowedActions);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TModel model)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
            {
                return Forbidden($"Create forbidden on {typeof(TModel)}");
            }

            try
            {
                Service.Create(model);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<TModel> Read(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return Forbidden($"Read forbidden on {typeof(TModel)}");
            }

            var entity = Service.Get(id);

            if (entity == null)
            {
                return NotFound($"{typeof(TModel)} {id} not found.");
            }

            return Ok(entity);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TModel>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return Forbidden($"Read forbidden on {typeof(TModel)}");
            }

            var entities = Service.GetAll();

            return Ok(entities);
        }

        [HttpPut("{id}")]
        public IActionResult Update(TId id, [FromBody] TModel model)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Update))
            {
                return Forbidden($"Update forbidden on {typeof(TModel)}");
            }

            try
            {
                Service.Update(id, model);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"{typeof(TModel)} {id} not found.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Delete))
            {
                return Forbidden($"Delete forbidden on {typeof(TModel)}");
            }

            try
            {
                Service.Delete(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"{typeof(TModel)} {id} not found.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        protected ObjectResult Forbidden(string message) => StatusCode(403, message);
    }
}
