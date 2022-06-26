using System;
using System.Collections.Generic;
using System.Reflection;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.API.Swagger;
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
        protected readonly CrudAction[]? CrudActions;

        public CrudController(TService service)
        {
            Service = service;
            CrudActions = GetType().GetCustomAttribute<AllowedActionsAttribute>()?.AllowedActions ?? Array.Empty<CrudAction>();
        }

        [Obsolete("CrudAction constructor is deprecated and will be removed in a future release. Use AllowedActionsAttribute instead")]
        public CrudController(TService service, params CrudAction[]? allowedActions)
        {
            Service = service;
            CrudActions = allowedActions;
        }

        [HttpPost]
        [SwaggerDocsFilter(CrudAction.Create)]
        public virtual IActionResult Create([FromBody] TModel model)
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
        [SwaggerDocsFilter(CrudAction.Read)]
        public virtual ActionResult<TModel> Read(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return Forbidden($"Read forbidden on {typeof(TModel)}");
            }

            var model = Service.Get(id);

            if (model == null)
            {
                return NotFound($"{typeof(TModel)} {id} not found.");
            }

            return Ok(model);
        }

        [HttpGet]
        [SwaggerDocsFilter(CrudAction.Read)]
        public virtual ActionResult<IEnumerable<TModel>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return Forbidden($"Read forbidden on {typeof(TModel)}");
            }

            var entities = Service.GetAll();

            return Ok(entities);
        }

        [HttpPut("{id}")]
        [SwaggerDocsFilter(CrudAction.Update)]
        public virtual IActionResult Update(TId id, [FromBody] TModel model)
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
        [SwaggerDocsFilter(CrudAction.Delete)]
        public virtual IActionResult Delete(TId id)
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
