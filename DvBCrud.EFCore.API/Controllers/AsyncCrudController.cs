using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using DvBCrud.Common.Api.Controllers;
using DvBCrud.Common.Api.CrudActions;
using DvBCrud.Common.Api.Swagger;
using DvBCrud.EFCore.Services;
using DvBCrud.EFCore.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.EFCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncCrudController<TId, TModel, TService> : CrudControllerBase<TModel>
        where TModel : BaseModel
        where TService : IService<TId, TModel>
    {
        protected readonly TService Service;
        protected readonly CrudAction[]? CrudActions;

        public AsyncCrudController(TService service)
        {
            Service = service;
            CrudActions = GetType().GetCustomAttribute<AllowedActionsAttribute>()?.AllowedActions ?? Array.Empty<CrudAction>();
        }

        [HttpPost]
        [SwaggerDocsFilter(CrudAction.Create)]
        public virtual async Task<IActionResult> Create([FromBody] TModel entity)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
            {
                return NotAllowed(HttpMethod.Post.Method);
            }

            try
            {
                await Service.CreateAsync(entity);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [SwaggerDocsFilter(CrudAction.Read)]
        public virtual async Task<ActionResult<TModel>> Read(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return NotAllowed(HttpMethod.Get.Method);
            }

            var entity = await Service.GetAsync(id);

            if (entity == null)
            {
                return NotFound($"{typeof(TModel)} {id} not found.");
            }

            return Ok(entity);
        }

        [HttpGet]
        [SwaggerDocsFilter(CrudAction.Read)]
        public virtual async Task<ActionResult<IEnumerable<TModel>>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return NotAllowed(HttpMethod.Get.Method);
            }

            var entities = await Task.Run(() => Service.GetAll());

            return Ok(entities);
        }

        [HttpPut("{id}")]
        [SwaggerDocsFilter(CrudAction.Update)]
        public virtual async Task<IActionResult> Update(TId id, [FromBody] TModel entity)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Update))
            {
                return NotAllowed(HttpMethod.Put.Method);
            }

            try
            {
                await Service.UpdateAsync(id, entity);
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
        public virtual async Task<IActionResult> Delete(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Delete))
            {
                return NotAllowed(HttpMethod.Delete.Method);
            }

            try
            {
                await Service.DeleteAsync(id);
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
