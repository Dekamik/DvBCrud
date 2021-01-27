using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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

            repository.Create(entity);
            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Create)} OK");
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateRange([FromBody]IEnumerable<TEntity> entities)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(CreateRange)} {entities.Count()} {nameof(TEntity)}");

            repository.CreateRange(entities);
            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(CreateRange)} OK");
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] TEntity entity, [FromQuery] bool createIfNotExists = false)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(Update)} {nameof(TEntity)}.Id = {entity}{(createIfNotExists ? ", createIfNotExists = true" : "")}");

            repository.Update(entity, createIfNotExists);
            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(Update)} OK");
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateRange([FromBody]IEnumerable<TEntity> entities, [FromQuery]bool createIfNotExists = false)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(UpdateRange)} {entities.Count()} {nameof(TEntity)}.Id = {string.Join(", ", entities)}{(createIfNotExists ? ", createIfNotExists = true" : "")}");

            repository.UpdateRange(entities, createIfNotExists);
            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(UpdateRange)} OK");
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

        [HttpDelete]
        public IActionResult DeleteRange([FromBody]IEnumerable<TId> id)
        {
            var guid = Guid.NewGuid();
            logger.LogDebug($"{guid}: {nameof(DeleteRange)} {nameof(TEntity)}.Id = {string.Join(", ", id)}");

            repository.DeleteRange(id);
            repository.SaveChanges();

            logger.LogDebug($"{guid}: {nameof(DeleteRange)} OK");
            return Ok();
        }
    }
}
