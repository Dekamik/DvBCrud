# DvBCrud.EFCore.API

Library for rapidly developing CRUD API-endpoints for database entities.

## How it works

DvBCrud.EFCore.API is a complement to DvBCrud.EFCore for streamlining development of CRUD APIs.

The `CRUDController` comes in two flavors: the synchronous `CRUDController` and the asynchronous `AsyncCRUDController`.
Depending on your needs and use-case, you may use one or both of them.

Both controller types implement the following actions:
- `void Create(TEntity entity)` Creates entity.
- `TEntity Read(TId id)` Gets entity with matching Id
- `IEnumerable<TEntity> ReadAll()` Gets all entities
- `void Update(TId id, TEntity entity)` Updates the matching entity
- `void Delete(TId id)` Deletes the matching entity

## Getting started

### 1. Create entity and repository

Follow [this guide](../DvBCrud.EFCore) to create your entities and your repository.

### 2. Create a CRUDController

Create the CRUDController for the entity and its repository

`AnyController.cs`
```
public class AnyController : CRUDController<AnyEntity, int, IAnyRepository, AnyDbContext>, IAnyCRUDController
{
    public AnyController(IAnyRepository anyRepository, ILogger logger) : base(anyRepository, logger)
    {

    }
}
```

### 3. Use it

Inject the controller in `Startup.cs` and you're good to go. The controller is now up and running on your application.

## Restricting actions

You can restrict which actions are permitted calling the overloaded constructor when defining your controllers.
When a forbidden action is requested, the endpoint will return a 403 FORBIDDEN response.

If no actions are provided to the constructor, the controller will default to allow all actions.

Here are some examples:

### Read-only endpoint

`AnyController.cs`
```cs
public class AnyController : CRUDController<AnyEntity, int, IAnyRepository, AnyDbContext>, IAnyReadOnlyController
{
    public AnyController(IAnyRepository repository, ILogger logger) : base(repository, logger, CRUDAction.Read)
    {

    }
}
```

### Non-deleteable endpoint

`AnyController.cs`
```cs
public class AnyController : CRUDController<AnyEntity, int, IAnyRepository, AnyDbContext>, IAnyReadOnlyController
{
    public AnyController(IAnyRepository repository, ILogger logger) : base(repository, logger, CRUDAction.Create CRUDAction.Read, CRUDAction.Update)
    {

    }
}
```
