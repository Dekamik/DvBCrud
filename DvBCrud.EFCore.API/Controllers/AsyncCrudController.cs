﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.API.Extensions;
using DvBCrud.EFCore.API.Swagger;
using DvBCrud.EFCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.EFCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncCrudController<TId, TModel, TService> : CrudControllerBase<TModel>
        where TModel : class
        where TService : IService<TId, TModel>
    {
        protected readonly TService Service;
        protected readonly CrudAction[]? CrudActions;

        public AsyncCrudController(TService service)
        {
            Service = service;
            CrudActions = GetType().GetCrudActions();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerDocsFilter(CrudAction.Create)]
        public virtual async Task<IActionResult> Create([FromBody] TModel model)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
            {
                return NotAllowed(HttpMethod.Post.Method);
            }

            try
            {
                var id = await Service.CreateAsync(model);
                var createdModel = await Service.GetAsync(id);
                return CreatedAtRoute(new { id }, createdModel);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerDocsFilter(CrudAction.Read)]
        public virtual async Task<ActionResult<IEnumerable<TModel>>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return NotAllowed(HttpMethod.Get.Method);
            }

            var entities = await Task.Run(() => Service.List());

            return Ok(entities);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerDocsFilter(CrudAction.Update)]
        public virtual async Task<IActionResult> Update(TId id, [FromBody] TModel model)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Update))
            {
                return NotAllowed(HttpMethod.Put.Method);
            }

            try
            {
                await Service.UpdateAsync(id, model);
                return NoContent();
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
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
                return NoContent();
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
