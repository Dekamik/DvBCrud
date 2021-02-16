# DvBCrud

DvBCrud is a collection of CRUD templating libraries.

This project aims to both standardize and enable rapid development of normal database operations and use-cases.

## Projects

### [DvBCrud.EFCore](https://github.com/Dekamik/DvBCrud.EFCore)
[![Build Status](https://travis-ci.com/Dekamik/DvBCrud.EFCore.svg?branch=master)](https://travis-ci.com/Dekamik/DvBCrud.EFCore)
[![Nuget](https://img.shields.io/nuget/v/DvBCrud.EFCore?label=DvBCrud.EFCore)](https://www.nuget.org/packages/DvBCrud.EFCore/)
[![Nuget](https://img.shields.io/nuget/v/DvBCrud.EFCore.API?label=DvBCrud.EFCore.API)](https://www.nuget.org/packages/DvBCrud.EFCore.API/)

EFCore implementation.

### [DvBCrud.MongoDB](https://github.com/Dekamik/DvBCrud.MongoDB)
[![Build Status](https://travis-ci.com/Dekamik/DvBCrud.MongoDB.svg?branch=master)](https://travis-ci.com/Dekamik/DvBCrud.MongoDB)
[![Nuget](https://img.shields.io/nuget/v/DvBCrud.MongoDB?label=DvBCrud.MongoDB)](https://www.nuget.org/packages/DvBCrud.MongoDB/)
[![Nuget](https://img.shields.io/nuget/v/DvBCrud.MongoDB.API?label=DvBCrud.MongoDB.API)](https://www.nuget.org/packages/DvBCrud.MongoDB.API/)

WORK IN PROGRESS.

MongoDB implementation.

## How it works

The library consists of base classes that implement common REST and CRUD functionality.

The developer defines repositories and controllers that inherit from a base class. 
The defined repositories and controllers will then inherit all or selected CRUD functionality from the base class.

For this to work, all entities must inherit from a BaseEntity class.

These libraries have a unified structure across all projects.

## Example: Customer in a restaurant app

This is an example implementation for a Customer endpoint that serves Customer data using DvBCrud.EFCore:

`Customer.cs`
```cs
public class Customer : BaseEntity<int>
{
    public string Name { get; set; }
    
    public DateTime Birthdate { get; set; }

    protected override void CopyImpl(BaseEntity<int> other)
    {
        var o = other as Customer;
        Name = o.Name;
        Birthdate = o.Birthdate;
    }
}
```

`CustomerRepository.cs`
```cs
public class CustomerRepository : Repository<Customer, int, RestaurantDbContext>
{
    public CustomerRepository(RestaurantDbContext dbContext, ILogger logger) : base(dbContext, logger)
    {

    }
}
```

`CustomerController.cs`
```cs
public class CustomerController : CRUDController<Customer, int, CustomerRepository, RestaurantDbContext>
{
    public CustomerController(CustomerRepository repository, ILogger logger) : base(repository, logger)
    {

    }
}
```

When `CustomerRepository` and `CustomerController` are registered in `Startup.cs`, these three classes generate complete REST functionality for Customer data. 
This Customer endpoint will allow Create, Read, Update and Delete actions on Customer entities.

## Example: Read-only endpoint

You want to write-protect your data? No problem. Simply define that only `CRUDAction.Read` is allowed in the overloaded constructor like below.

`ReadOnlyCustomerController.cs`
```cs
public class ReadOnlyCustomerController : CRUDController<Customer, int, CustomerRepository, RestaurantDbContext>
{
    public ReadOnlyCustomerController(CustomerRepository repository, ILogger logger) : base(repository, logger, CRUDAction.Read)
    {

    }
}
```

The endpoint above will return a 403 FORBIDDEN response on requests for Create, Update and Delete actions.

## Example: Endpoint with only Create, Read and Update actions

You can also define a selection of `CRUDAction`s to allow in the overloaded constructor like below.

`ReadOnlyCustomerController.cs`
```cs
public class WritableCustomerController : CRUDController<Customer, int, CustomerRepository, RestaurantDbContext>
{
    public WritableCustomerController(CustomerRepository repository, ILogger logger) : base(repository, logger, CRUDAction.Create, CRUDAction.Read, CRUDAction.Update)
    {

    }
}
```

The endpoint above will return a 403 FORBIDDEN response on requests for the Delete action only.
