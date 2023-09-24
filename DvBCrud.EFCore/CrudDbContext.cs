using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DvBCrud.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.EFCore;

public abstract class CrudDbContext : DbContext
{
    private readonly IDateTimeOffsetFactory _dateTimeOffsetFactory;

    protected CrudDbContext()
    {
        _dateTimeOffsetFactory = new DefaultDateTimeOffsetFactory();
    }

    protected CrudDbContext(IDateTimeOffsetFactory dateTimeOffsetFactory)
    {
        _dateTimeOffsetFactory = dateTimeOffsetFactory;
    }

    protected CrudDbContext(DbContextOptions options) : base(options)
    {
        _dateTimeOffsetFactory = new DefaultDateTimeOffsetFactory();
    }
    
    protected CrudDbContext(DbContextOptions options, IDateTimeOffsetFactory dateTimeOffsetFactory) : base(options)
    {
        _dateTimeOffsetFactory = dateTimeOffsetFactory;
    }
    
    public override int SaveChanges()
    {
        ModifyTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
    {
        ModifyTimestamps();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        ModifyTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ModifyTimestamps()
    {
        var utcNow = _dateTimeOffsetFactory.UtcNow();
        var addedEntries = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Added)
            .Select(x => x.Entity);

        foreach (var entry in addedEntries)
        {
            if (entry is ICreatedAt addedModel)
            {
                addedModel.CreatedAt = utcNow;
            }
            if (entry is IModifiedAt modifiedModel)
            {
                modifiedModel.ModifiedAt = utcNow;
            }
        }

        var modifiedEntries = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var entry in modifiedEntries)
        {
            if (entry is IModifiedAt model)
            {
                model.ModifiedAt = utcNow;
            }
        }
    }
}