using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.EFCore.Tests.Mocks;

[ExcludeFromCodeCoverage]
public class AnyDbContext : CrudDbContext
{
    public AnyDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<AnyEntity> AnyEntities { get; set; }
}