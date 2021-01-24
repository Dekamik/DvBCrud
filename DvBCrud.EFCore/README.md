# DvBCrud.EFCore

The core implementation for handling repositories and entities using Entity Framework Core.

## How it works

DvBCrud.EFCore streamlines the process of developing a Code-First database by implementing basic CRUD functionality through using generics and polymorphism.

The basic workflow goes like this:

1. Define your entity by inheriting BaseEntity or BaseAuditedEntity.
2. Add it to your DbContext.
3. Define the repository and its functionality by inheriting ReadOnlyRepository, Repository or AuditedRepository (requires BaseAuditedEntity-derived entity).

And that's it. Migrate using EFCore and you have fully functional and fully tested database CRUD couplings for your application. 
All CRUD functionality is already written and available.

## Getting started

When you've installed the library, below is an example for defining a Repository for AnyEntity

### 1. Create the entity

Create the entity by defining its Id type (`int`).

`AnyEntity.cs`
```cs
public class AnyEntity : BaseEntity<int>
{
    public string AnyString { get; set; }

    protected override void CopyImpl(BaseEntity<int> other)
    {
        var o = other as AnyEntity;
        AnyString = o.AnyString;
    }
}
```

### 2. Add it to your DbContext

`AnyDbContext.cs`
```cs
public class AnyDbContext : DbContext
{
    public AnyDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<AnyEntity> AnyEntities { get; set; }
}
```

### 3. Create the repository

Create the repository by defining its entity type (`AnyEntity`), the entity's Id type (`int`) and the DbContext type (`AnyDbContext`).

`AnyRepository.cs`
```cs
public class AnyRepository : Repository<AnyEntity, int, AnyDbContext>
{
    public AnyRepository(AnyDbContext dbContext, ILogger logger) : base(dbContext, logger)
    {

    }
}
```

### 4. Use it

After you've injected a new AnyRepository into your application's `Startup.cs`, you can use it like so:

`AnyService.cs`
```cs
public class AnyService
{
    private readonly IAnyRepository anyRepository;

    public AnyClass(IAnyRepository anyRepository)
    {
        this.anyRepository = anyRepository;
    }

    public void CreateAnyEntity(string str)
    {
        var entity = new AnyEntity 
        {
            AnyString = str
        };

        anyRepository.Create(entity);
    }
}
```
