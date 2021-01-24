using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DvBCrud.EFCore.Mocks.DbContexts
{
    /// <summary>
    /// DbContext provider for testing purposes
    /// </summary>
    public class AnyDbContextProvider : IDisposable
    {
        public AnyDbContext DbContext { get; set; }

        public AnyDbContextProvider(string DbName)
        {
            var options = new DbContextOptionsBuilder<AnyDbContext>()
                .UseInMemoryDatabase(DbName)
                .Options;
            DbContext = new AnyDbContext(options);
        }

        public void Dispose()
        {
            // Clean up after each test
            foreach (var entity in DbContext.AnyEntities)
            {
                DbContext.AnyEntities.Remove(entity);
            }

            foreach (var entity in DbContext.AnyNullableIdEntities)
            {
                DbContext.AnyNullableIdEntities.Remove(entity);
            }

            foreach (var entity in DbContext.AnyAuditedEntities)
            {
                DbContext.AnyAuditedEntities.Remove(entity);
            }

            DbContext.SaveChanges();
        }

        public void Mock<TEntity>(params TEntity[] entities) where TEntity : BaseEntity<int>
        {
            DbContext.Set<TEntity>().AddRange(entities);
            DbContext.SaveChanges();
        }
    }
}
