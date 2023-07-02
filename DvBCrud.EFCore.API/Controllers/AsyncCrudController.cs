using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DvBCrud.EFCore.API.Extensions;
using DvBCrud.EFCore.API.Permissions;
using DvBCrud.EFCore.API.Swagger;
using DvBCrud.EFCore.Exceptions;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable MemberCanBePrivate.Global

namespace DvBCrud.EFCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncCrudController<TId, TModel, TRepository> : CrudControllerBase<TModel>
        where TModel : class
        where TRepository : IRepository<TId, TModel>
    {
        protected readonly TRepository CrudHandler;
        protected readonly CrudActions CrudActions;

        protected AsyncCrudController(TRepository crudHandler)
        {
            CrudHandler = crudHandler;
            CrudActions = GetType().GetCrudActions();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerDocsFilter(CrudActions.Create)]
        public virtual async Task<IActionResult> Create([FromBody] TModel model)
        {
            if (!CrudActions.IsActionAllowed(CrudActions.Create))
            {
                return NotAllowed(HttpMethod.Post.Method);
            }

            try
            {
                var id = await CrudHandler.CreateAsync(model);
                var createdModel = await CrudHandler.GetAsync(id);
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
        [SwaggerDocsFilter(CrudActions.Read)]
        public virtual async Task<ActionResult<TModel>> Read(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudActions.Read))
            {
                return NotAllowed(HttpMethod.Get.Method);
            }

            try
            {
                return Ok(await CrudHandler.GetAsync(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerDocsFilter(CrudActions.Read)]
        public virtual async Task<ActionResult<IEnumerable<TModel>>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudActions.Read))
            {
                return NotAllowed(HttpMethod.Get.Method);
            }

            try
            {
                return Ok(await Task.Run(() => CrudHandler.List()));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerDocsFilter(CrudActions.Update)]
        public virtual async Task<IActionResult> Update(TId id, [FromBody] TModel model)
        {
            if (!CrudActions.IsActionAllowed(CrudActions.Update))
            {
                return NotAllowed(HttpMethod.Put.Method);
            }

            try
            {
                await CrudHandler.UpdateAsync(id, model);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"{typeof(TModel)} {id} not found.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerDocsFilter(CrudActions.Delete)]
        public virtual async Task<IActionResult> Delete(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudActions.Delete))
            {
                return NotAllowed(HttpMethod.Delete.Method);
            }

            try
            {
                await CrudHandler.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"{typeof(TModel)} {id} not found.");
            }
        }
    }
}
