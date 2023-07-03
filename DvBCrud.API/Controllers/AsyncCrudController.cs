using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DvBCrud.API.Permissions;
using DvBCrud.API.Swagger;
using DvBCrud.Shared;
using DvBCrud.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable MemberCanBePrivate.Global

namespace DvBCrud.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncCrudController<TId, TModel, TRepository> : CrudControllerBase<TModel>
        where TModel : class
        where TRepository : IRepository<TId, TModel>
    {
        protected readonly TRepository Repository;
        protected readonly CrudActions CrudActions;

        protected AsyncCrudController(TRepository repository)
        {
            Repository = repository;
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
                var id = await Repository.CreateAsync(model);
                var createdModel = await Repository.GetAsync(id);
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
                return Ok(await Repository.GetAsync(id));
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

            return Ok(await Task.Run(() => Repository.List()));
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
                await Repository.UpdateAsync(id, model);
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
                await Repository.DeleteAsync(id);
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
