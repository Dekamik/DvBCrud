using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using DvBCrud.Common.Api.Controllers;
using DvBCrud.Common.Api.CrudActions;
using DvBCrud.Common.Api.Swagger;
using DvBCrud.EFCore.API.Helpers;
using DvBCrud.EFCore.Services;
using DvBCrud.EFCore.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.EFCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class CrudController<TId, TModel, TService> : CrudControllerBase<TModel>
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

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [SwaggerDocsFilter(CrudAction.Create)]
        public virtual IActionResult Create([FromBody] TModel model)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
            {
                return NotAllowed(HttpMethod.Post.Method);
            }

            try
            {
                var id = Service.Create(model);
                var url = UrlHelper.GetResourceUrl(Request, id);
                return Created(url, null);
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
                return NotAllowed(HttpMethod.Get.Method);
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
                return NotAllowed(HttpMethod.Get.Method);
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
                return NotAllowed(HttpMethod.Put.Method);
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
                return NotAllowed(HttpMethod.Delete.Method);
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
    }
}
