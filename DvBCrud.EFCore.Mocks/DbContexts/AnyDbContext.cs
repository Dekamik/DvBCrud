using DvBCrud.EFCore.Mocks.Entities;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.EFCore.Mocks.DbContexts
{
    public class AnyDbContext : DbContext
    {
        public AnyDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AnyEntity> AnyEntities { get; set; }

        public DbSet<AnyNullableIdEntity> AnyNullableIdEntities { get; set; }

        public DbSet<AnyAuditedEntity> AnyAuditedEntities { get; set; }
    }
}
