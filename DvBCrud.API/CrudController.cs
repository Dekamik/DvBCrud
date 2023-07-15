using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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

public abstract class CrudController<TId, TModel, TRepository, TFilter> : CrudControllerBase<TModel>
    where TModel : class
    where TRepository : IRepository<TId, TModel, TFilter>
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
    public virtual ActionResult<Response<TModel>> Create([FromBody] TModel model)
    {
        if (!CrudActions.IsActionAllowed(CrudActions.Create))
        {
            return NotAllowed(HttpMethod.Post.Method);
        }

        try
        {
            var id = Repository.Create(model);
            var createdModel = Repository.Get(id);
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
    public virtual ActionResult<Response<TModel>> Read(TId id)
    {
        if (!CrudActions.IsActionAllowed(CrudActions.ReadById))
        {
            return NotAllowed(HttpMethod.Get.Method);
        }

        try
        {
            var model = Repository.Get(id);
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
    public virtual ActionResult<Response<IEnumerable<TModel>>> List([FromQuery] TFilter filter)
    {
        if (!CrudActions.IsActionAllowed(CrudActions.List))
        {
            return NotAllowed(HttpMethod.Get.Method);
        }

        var models = Repository.List(filter);
        return Ok(new Response<IEnumerable<TModel>>(models));
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