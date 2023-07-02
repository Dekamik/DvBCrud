using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Mocks.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.EFCore.Mocks.Core.DbContexts
{
    [ExcludeFromCodeCoverage]
    public class AnyDbContext : CrudDbContext
    {
        public AnyDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AnyEntity> AnyEntities { get; set; }
    }
}
