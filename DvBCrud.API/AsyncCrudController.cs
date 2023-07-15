using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DvBCrud.API.Swagger;
using DvBCrud.Shared;
using DvBCrud.Shared.Exceptions;
using DvBCrud.Shared.Permissions;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
// ReSharper disable ConvertIfStatementToReturnStatement
// ReSharper disable VirtualMemberNeverOverridden.Global
// ReSharper disable MemberCanBePrivate.Global

namespace DvBCrud.API;

public abstract class AsyncCrudController<TId, TModel, TRepository, TFilter> : CrudControllerBase<TModel>
    where TModel : class
    where TRepository : IRepository<TId, TModel, TFilter>
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
    public virtual async Task<ActionResult<Response<TModel>>> Create([FromBody] TModel model)
    {
        if (!CrudActions.IsActionAllowed(CrudActions.Create))
        {
            return NotAllowed(HttpMethod.Post.Method);
        }

        try
        {
            var id = await Repository.CreateAsync(model);
            var createdModel = await Repository.GetAsync(id);
            return CreatedAtRoute(new { id }, new Response<TModel>(createdModel));
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [SwaggerDocsFilter(CrudActions.ReadById)]
    public virtual async Task<ActionResult<Response<TModel>>> Read(TId id)
    {
        if (!CrudActions.IsActionAllowed(CrudActions.ReadById))
        {
            return NotAllowed(HttpMethod.Get.Method);
        }

        try
        {
            var model = await Repository.GetAsync(id);
            return Ok(new Response<TModel>(model));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [SwaggerDocsFilter(CrudActions.ReadById)]
    public virtual async Task<ActionResult<Response<IEnumerable<TModel>>>> List([FromQuery] TFilter filter)
    {
        if (!CrudActions.IsActionAllowed(CrudActions.List))
        {
            return NotAllowed(HttpMethod.Get.Method);
        }

        var models = Repository.List(filter);
        return Ok(await Task.Run(() => new Response<IEnumerable<TModel>>(models)));
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
        catch (NotFoundException)
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
        catch (NotFoundException)
        {
            return NotFound($"{typeof(TModel)} {id} not found.");
        }
    }
}