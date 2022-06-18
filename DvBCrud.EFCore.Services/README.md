# DvBCrud.EFCore.Services

Templates for generating services, models and converters.

## Table of contents

- [How it works](#How-it-works)
- [Getting started](#Getting-started)
    * [1. Create entity and repository](#1-create-entity-and-repository)
    * [2. Create model](#2-create-model)
    * [3. Create converter](#3-create-converter)
    * [4. Create service](#4-create-service)
    * [5. Use it](#5-use-it)

## How it works

DvBCrud.EFCore.Services is a couple of classes that acts as the link between the data layer and the controller layer.

The services were created to give the developer control over the data being exposed and how these models are built.

## Getting started

### 1. Create entity and repository

Follow [this guide](../DvBCrud.EFCore) to create your entities and your repository.

### 2. Create model

`AnyModel.cs`
```csharp
public class AnyModel : BaseModel
{
    public string AnyString { get; set; }
}
```

### 3. Create converter

`IAnyConverter.cs`
```csharp
public interface IAnyConverter : IConverter<AnyEntity, AnyModel>
{
}
```

`AnyConverter.cs`
```csharp
public class AnyConverter : Converter<AnyEntity, AnyModel>, IAnyConverter
{
    private readonly IAnyRepository _repository;
    
    public AnyConverter(IAnyRepository repository)
    {
        _repository = repository;
    }

    public override AnyModel ToModel(AnyEntity entity)
    {
        return new AnyModel
        {
            Id = entity.Id
            AnyString = entity.AnyString
        };
    }
    
    public override AnyEntity ToEntity(AnyModel model)
    {
        var entity = _repository.Get(model.Id) ?? new AnyEntity();
        
        entity.AnyString = model.AnyString;
        
        return entity;
    }
}
```

### 4. Create service

`IAnyService.cs`
```csharp
public interface IAnyService : IService<int, AnyModel>
{
}
```

`AnyService.cs`
```csharp
public class AnyService : Service<AnyEntity, int, IAnyRepository, AnyModel, IAnyConverter>, IAnyService
{
    public AnyService(IAnyRepository repository, IAnyConverter converter) : base(repository, converter)
    {
    }
}
```

### 5. Use it

After all the steps above are taken you're good to go.
