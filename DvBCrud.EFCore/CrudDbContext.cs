using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.EFCore;

public abstract class CrudDbContext : DbContext
{
    public CrudDbContext()
    {
        
    }

    public CrudDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public override int SaveChanges()
    {
        ModifyTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        ModifyTimestamps();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ModifyTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ModifyTimestamps()
    {
        var addedEntries = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Added)
            .Select(x => x.Entity);

        foreach (var entry in addedEntries)
        {
            var utcNow = DateTimeOffset.UtcNow;
            switch (entry)
            {
                case ICreatedAt addedModel:
                    addedModel.CreatedAt = utcNow;
                    break;
                case IModifiedAt modifiedModel:
                    modifiedModel.ModifiedAt = utcNow;
                    break;
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
                model.ModifiedAt = DateTimeOffset.UtcNow;
            }
        }
    }
}