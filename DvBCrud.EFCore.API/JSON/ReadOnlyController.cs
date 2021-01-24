using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DvBCrud.EFCore.API.JSON
{
    public abstract class ReadOnlyController<TEntity, TId, TRepository, TDbContext> : ControllerBase, IReadOnlyController<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TRepository : IReadOnlyRepository<TEntity, TId>
        where TDbContext : DbContext
    {
        internal readonly TRepository repository;
        internal readonly ILogger logger;

        public ReadOnlyController(TRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet, Route("{id}")]
        public ActionResult<TEntity> Read(TId id)
        {
            logger.LogTrace($"Request recieved at {nameof(Read)} for {nameof(TRepository)} ({nameof(TEntity)})");

            TEntity entity = repository.Get(id);

            return Ok(entity);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TEntity>> ReadAll()
        {
            logger.LogTrace($"Request recieved at {nameof(ReadAll)} for {nameof(TRepository)} ({nameof(TEntity)})");

            var entities = repository.GetAll();

            return Ok(entities.AsEnumerable());
        }
    }
}
