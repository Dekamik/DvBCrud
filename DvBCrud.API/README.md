# DvBCrud.EFCore.API

Library for rapidly developing CRUD API-endpoints for database entities.

## Table of Contents

- [How it works](#How-it-works)
- [Getting started](#Getting-started)
    * [1. Create entity and repository](#1.-Create-entity-and-repository)
    * [2. Create service, model and converter](#2.-Create-service,-model-and-converter)
    * [3. Create a CRUDController](#3.-Create-a-CRUDController)
    * [4. Use it](#4.-Use-it)
- [Restricting actions](#Restricting-actions)
    * [Read-only endpoint](#read-only-endpoint)
    * [Non-deletable endpoint](#non-deletable-endpoint)
    * [Hide restricted actions in Swagger Docs](#hide-restricted-actions-in-swagger-docs)
- [API Example](#api-example)

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

### 2. Create service, model and converter

Follow [this guide](../DvBCrud.EFCore.Services) to create your services, models and converters

### 3. Create a CRUDController

Create the CRUDController for the entity and its repository

`AnyController.cs`
```csharp
public class AnyController : CRUDController<int, AnyModel, IAnyService>, IAnyController
{
    public AnyController(IAnyService anyService) : base(anyService)
    {
    }
}
```

### 4. Use it

Inject the controller in `Startup.cs` and you're good to go. The controller is now up and running on your application.

## Restricting actions

You can restrict which actions are permitted by adding the CrudActionAttribute on the class.
When a forbidden action is requested, the endpoint will return a 403 FORBIDDEN response.

If the attribute is not defined, the controller will default to allow all actions.

Here are some examples:

### Read-only endpoint

`AnyController.cs`
```csharp
[AllowedActions(CRUDAction.Read)]
public class AnyController : CRUDController<int, AnyModel, IAnyService>, IAnyController
{
    public AnyController(IAnyService anyService) : base(anyService)
    {
    }
}
```

### Non-deletable endpoint

`AnyController.cs`
```csharp
[AllowedActions(CRUDAction.Create CRUDAction.Read, CRUDAction.Update)]
public class AnyController : CRUDController<int, AnyModel, IAnyService>, IAnyController
{
    public AnyController(IAnyService anyService) : base(anyService)
    {
    }
}
```

### Hide restricted actions in Swagger Docs

To hide the restricted actions in Swagger UI, you need to add the `SwaggerDocsFilter` when declaring Swagger in Program.cs like so:

```csharp
[...]
services.AddSwaggerGen(c => {
    c.DocumentFilter<SwaggerDocsFilter>();
}
[...]
```

## API Example

You can find a fully-implemented working API example in the [DvBCrud.EFCore.API.Tests.Web](../DvBCrud.EFCore.API.Tests.Web) project.
