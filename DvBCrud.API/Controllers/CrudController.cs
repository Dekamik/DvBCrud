using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
    public abstract class CrudController<TId, TModel, TRepository> : CrudControllerBase<TModel>
        where TModel : class
        where TRepository : IRepository<TId, TModel>
    {
        protected readonly TRepository Repository;
        protected readonly CrudActions CrudActions;

        protected CrudController(TRepository repository)
        {
            Repository = repository;
            CrudActions = GetType().GetCrudActions();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [SwaggerDocsFilter(CrudActions.Create)]
        public virtual IActionResult Create([FromBody] TModel model)
        {
            if (!CrudActions.IsActionAllowed(CrudActions.Create))
            {
                return NotAllowed(HttpMethod.Post.Method);
            }

            try
            {
                var id = Repository.Create(model);
                var createdModel = Repository.Get(id);
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
        public virtual ActionResult<TModel> Read(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudActions.Read))
            {
                return NotAllowed(HttpMethod.Get.Method);
            }

            try
            {
                return Ok(Repository.Get(id));
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
        public virtual ActionResult<IEnumerable<TModel>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudActions.Read))
            {
                return NotAllowed(HttpMethod.Get.Method);
            }

            return Ok(Repository.List());
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerDocsFilter(CrudActions.Update)]
        public virtual IActionResult Update(TId id, [FromBody] TModel model)
        {
            if (!CrudActions.IsActionAllowed(CrudActions.Update))
            {
                return NotAllowed(HttpMethod.Put.Method);
            }

            try
            {
                Repository.Update(id, model);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [SwaggerDocsFilter(CrudActions.Delete)]
        public virtual IActionResult Delete(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudActions.Delete))
            {
                return NotAllowed(HttpMethod.Delete.Method);
            }

            try
            {
                Repository.Delete(id);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
