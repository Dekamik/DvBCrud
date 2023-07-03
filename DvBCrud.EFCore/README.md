# DvBCrud.EFCore

CRUD functionality using Entity Framework Core.

## Table of Contents

- [How it works](#How-it-works)
- [Getting started](#Getting-started)
    * [1. Create the entity](#1-create-the-entity)
    * [2. Add it to your DbContext](#2-add-it-to-your-dbcontext)
    * [3. Create the model](#3-create-the-model)
    * [4. Create the mapper](#4-create-the-mapper)
    * [5. Create the repository](#5-create-the-repository)
    * [6. Use it](#6-use-it)

## How it works

DvBCrud.EFCore streamlines the process of developing a Code-First database by implementing basic CRUD functionality through using generics and polymorphism.

All CRUD functionality is already written, fully tested and available.

## Getting started

When you've installed the library, below is an example for defining a `Repository` for `AnyEntity`.

### 1. Create the entity

Create the entity by defining its Id type (`int`).

`AnyEntity.cs`
```csharp
public class AnyEntity : IEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]]
    public long Id { get; set; }
    
    public string? AnyString { get; set; }
}
```

### 2. Add it to your DbContext

`AnyDbContext.cs`
```csharp
public class AnyDbContext : CrudDbContext
{
    public AnyDbContext()
    {
    }

    public DbSet<AnyEntity> AnyEntities { get; set; }
}
```

### 3. Create the model

```csharp
public class AnyModel
{
    public long Id { get; set; } = "";
    public string? AnyString { get; set; }
}
```

### 4. Create the mapper

```csharp
public class AnyMapper : IAnyMapper
{
    public AnyModel ToModel(AnyEntity entity) =>
        new()
        {
            Id = entity.Id,
            AnyString = entity.AnyString
        };

    public AnyEntity ToEntity(AnyModel model) =>
        new()
        {
            Id = model.Id,
            AnyString = model.AnyString
        };

    public void UpdateEntity(AnyEntity source, AnyEntity destination)
    {
        destination.AnyString = source.AnyString;
    }
}
```

### 5. Create the repository

Create the repository by defining its entity type (`AnyEntity`), the entity's Id type (`long`) and the DbContext type (`AnyDbContext`).

`AnyRepository.cs`
```csharp
public class AnyRepository : Repository<AnyEntity, long, AnyDbContext, AnyMapper, AnyModel>
{
    public AnyRepository(AnyDbContext context, AnyMapper mapper) : base(context, mapper)
    {
    }
}
```

### 6. Use it

After you've injected a new AnyRepository into your application's `Startup.cs`, you can use it like so:

`AnyService.cs`
```csharp
public class AnyService
{
    private readonly AnyRepository _anyRepository;

    public AnyClass(AnyRepository anyRepository)
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
