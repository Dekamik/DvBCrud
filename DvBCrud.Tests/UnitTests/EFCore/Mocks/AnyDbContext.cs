using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.Tests.UnitTests.EFCore.Mocks;

[ExcludeFromCodeCoverage]
public class AnyDbContext : CrudDbContext
{
    public AnyDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<AnyEntity> AnyEntities { get; set; } = null!;
}