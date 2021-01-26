using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<TEntity>> Read([FromQuery]TId id)
        {
            logger.LogTrace($"{nameof(Read)} request recieved for a {nameof(TEntity)}.Id = {id}");

            TEntity entity = await repository.Get(id);

            return Ok(entity);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TEntity>> Read([FromBody] IEnumerable<TId> ids)
        {
            logger.LogTrace($"{nameof(Read)} request recieved for {ids.Count()} {nameof(TEntity)}.Id = {string.Join(", ", ids)}");

            IEnumerable<TEntity> entities = repository.GetRange(ids);

            return Ok(entities);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TEntity>> ReadAll()
        {
            logger.LogTrace($"{nameof(Read)} request recieved for all {nameof(TEntity)}");

            IEnumerable<TEntity> entities = repository.GetAll();

            return Ok(entities);
        }
    }
}
