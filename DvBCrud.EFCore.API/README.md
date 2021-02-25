# DvBCrud.EFCore.API

Library for rapidly developing CRUD API-endpoints for database entities.

## Table of Contents

- [How it works](#How-it-works)
- [Getting started](#Getting-started)
    * [1. Create entity and repository](#1.-Create-entity-and-repository)
    * [2. Create a CRUDController](#2.-Create-a-CRUDController)
    * [3. Use it](#3.-Use-it)
- [Restricting actions](#Restricting-actions)
    * [Read-only endpoint](#Read-only-endpoint)
    * [Non-deleteable endpoint](#Non-deleteable-endpoint)

## How it works

DvBCrud.EFCore.API is a complement to DvBCrud.EFCore for streamlining development of CRUD APIs.

The `CRUDController` comes in two flavors: the synchronous `CRUDController` and the asynchronous `AsyncCRUDController`.
Depending on your needs and use-case, you may use one or both of them.

Both controller types implement the following actions:
* CREATE: `POST /customer/`
* READ: `GET /customer/{id}`
* READ ALL: `GET /customer/`
* UPDATE: `PUT /customer/{id}`
* DELETE: `DELETE /customer/{id}`

## Getting started

### 1. Create entity and repository

Follow [this guide](../DvBCrud.EFCore) to create your entities and your repository.

### 2. Create a CRUDController

Create the CRUDController for the entity and its repository

`AnyController.cs`
```cs
public class AnyController : CRUDController<AnyEntity, int, IAnyRepository, AnyDbContext>, IAnyController
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
public class AnyController : CRUDController<AnyEntity, int, IAnyRepository, AnyDbContext>, IAnyController
{
    public AnyController(IAnyRepository repository, ILogger logger) : base(repository, logger, CRUDAction.Read)
    {
    }
}
```

### Non-deleteable endpoint

`AnyController.cs`
```cs
public class AnyController : CRUDController<AnyEntity, int, IAnyRepository, AnyDbContext>, IAnyController
{
    public AnyController(IAnyRepository repository, ILogger logger) : base(repository, logger, CRUDAction.Create CRUDAction.Read, CRUDAction.Update)
    {
    }
}
```
