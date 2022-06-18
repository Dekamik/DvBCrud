﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.Services;
using DvBCrud.EFCore.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.EFCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncCrudController<TId, TModel, TService> : ControllerBase
        where TModel : BaseModel
        where TService : IService<TId, TModel>
    {
        protected readonly TService Service;
        protected readonly CrudActionPermissions CrudActions;

        public AsyncCrudController(TService service)
        {
            Service = service;
            CrudActions = new CrudActionPermissions();
        }

        public AsyncCrudController(TService service, params CrudAction[]? allowedActions)
        {
            Service = service;
            CrudActions = new CrudActionPermissions(allowedActions);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TModel entity)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Create))
            {
                return Forbidden($"Create forbidden on {typeof(TModel)}");
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
        public virtual async Task<ActionResult<TModel>> Read(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return Forbidden($"Read forbidden on {typeof(TModel)}");
            }

            var entity = await Service.GetAsync(id);

            if (entity == null)
            {
                return NotFound($"{typeof(TModel)} {id} not found.");
            }

            return Ok(entity);
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TModel>>> ReadAll()
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Read))
            {
                return Forbidden($"Read forbidden on {typeof(TModel).Name}");
            }

            var entities = await Task.Run(() => Service.GetAll());

            return Ok(entities);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(TId id, [FromBody] TModel entity)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Update))
            {
                return Forbidden($"Update forbidden on {typeof(TModel)}");
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
        public virtual async Task<IActionResult> Delete(TId id)
        {
            if (!CrudActions.IsActionAllowed(CrudAction.Delete))
            {
                return Forbidden($"Delete forbidden on {typeof(TModel)}");
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

        protected ObjectResult Forbidden(string message) => StatusCode(403, message);
    }
}