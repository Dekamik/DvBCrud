# DvBCrud.EFCore

The core implementation for handling repositories and entities using Entity Framework Core.

## Table of Contents

- [How it works](#How-it-works)
- [Getting started](#Getting-started)
    * [1. Create the entity](#1.-Create-the-entity)
    * [2. Add it to your DbContext](#2.-Add-it-to-your-DbContext)
    * [3. Create the repository](#3.-Create-the-repository)
    * [4. Use it](#4.-Use-it)

## How it works

DvBCrud.EFCore streamlines the process of developing a Code-First database by implementing basic CRUD functionality through using generics and polymorphism.

The basic workflow goes like this:

1. Define your entity by inheriting from `BaseEntity`.
2. Add it to your `DbContext`.
3. Define the repository and its functionality by inheriting `Repository`.

And that's it. Migrate using EFCore and you have fully functional and fully tested database CRUD couplings for your application. 
All CRUD functionality is already written and available.

## Getting started

When you've installed the library, below is an example for defining a `Repository` for `AnyEntity`.

### 1. Create the entity

Create the entity by defining its Id type (`int`).

`AnyEntity.cs`
```csharp
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
```csharp
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

`IAnyRepository.cs`
```csharp
public interface IAnyRepository : IRepository<AnyEntity, int>
{
}
```

`AnyRepository.cs`
```csharp
public class AnyRepository : Repository<AnyEntity, int, AnyDbContext>, IAnyRepository
{
    public AnyRepository(AnyDbContext dbContext, ILogger logger) : base(dbContext, logger)
    {

    }
}
```

### 4. Use it

After you've injected a new AnyRepository into your application's `Startup.cs`, you can use it like so:

`AnyService.cs`
```csharp
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
