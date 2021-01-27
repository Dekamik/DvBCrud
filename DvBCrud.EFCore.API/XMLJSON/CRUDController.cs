using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace DvBCrud.EFCore.API.XMLJSON
{
    public abstract class CRUDController<TEntity, TId, TRepository, TDbContext> : ReadOnlyController<TEntity, TId, TRepository, TDbContext>, ICRUDController<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TRepository : IRepository<TEntity, TId>
        where TDbContext : DbContext
    {
        public CRUDController(TRepository repository, ILogger logger) : base(repository, logger)
        {

        }

        [HttpPost]
        public IActionResult Create([FromBody] TEntity entity)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Create)} {nameof(TEntity)}");

            // Id must NOT be predefined
            if (!entity.Id.Equals(default(TId)))
            {
                string message = $"{nameof(TEntity)}.Id must NOT be predefined.";
                logger.LogDebug($"{guid}: {nameof(Create)} BAD REQUEST - {message}");
                return BadRequest(message);
            }

            repository.Create(entity);
            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Create)} OK");
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] TEntity entity, [FromQuery] bool createIfNotExists = false)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Update)} {nameof(TEntity)}.Id = {entity}{(createIfNotExists ? ", createIfNotExists = true" : "")}");

            // Id must be predefined
            if (entity.Id.Equals(default(TId)))
            {
                string message = $"{nameof(TEntity)}.Id must be defined.";
                logger.LogDebug($"{guid}: {nameof(Update)} BAD REQUEST - {message}");
                return BadRequest(message);
            }

            repository.Update(entity, createIfNotExists);
            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Update)} OK");
            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public IActionResult Delete([FromQuery]TId id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Delete)} {nameof(TEntity)}.Id = {id}");

            repository.Delete(id);
            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Delete)} OK");
            return Ok();
        }
    }
}
