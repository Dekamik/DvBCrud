using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DvBCrud.EFCore.API.JSON
{
    public abstract class ReadOnlyController<TId, TEntity, TRepository, TDbContext> : ControllerBase, IReadOnlyController<TId>
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
        public IActionResult Read(TId id)
        {
            logger.LogTrace($"Request recieved at {nameof(Read)} for {nameof(TRepository)} ({nameof(TEntity)})");

            TEntity entity = repository.Get(id);

            return Ok(entity);
        }

        [HttpGet]
        public IActionResult ReadAll()
        {
            logger.LogTrace($"Request recieved at {nameof(ReadAll)} for {nameof(TRepository)} ({nameof(TEntity)})");

            IQueryable<TEntity> entities = repository.GetAll();

            return Ok(entities);
        }
    }
}
