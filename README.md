# DvBCrud

DvBCrud is a collection of CRUD templating libraries.

This project aims to both standardize and enable rapid development of normal database operations and use-cases.

## Projects

### [DvBCrud.EFCore](https://github.com/Dekamik/DvBCrud.EFCore)
[![Build Status](https://travis-ci.com/Dekamik/DvBCrud.EFCore.svg?branch=master)](https://travis-ci.com/Dekamik/DvBCrud.EFCore)

EFCore implementation.

### [DvBCrud.MongoDB](https://github.com/Dekamik/DvBCrud.MongoDB)
[![Build Status](https://travis-ci.com/Dekamik/DvBCrud.MongoDB.svg?branch=master)](https://travis-ci.com/Dekamik/DvBCrud.MongoDB)

WORK IN PROGRESS.

MongoDB implementation.

## How it works

The library consists of some base classes that implement common REST and CRUD functionality.

The developer only has to define repositories and controllers that inherit from the base classes. 
Once that is done, the defined repositories and controllers inherit all functionality from the base classes.

The only caveat is that all entities must inherit from one of the BaseEntity classes.

These libraries have a unified logic that work the same across all projects:

### Entities/Models

The object that represents an entry in the database.

### Repositories

The DTO object that allow developers to manipulate entities using CRUD actions.

### CRUDControllers

The API endpoint that allow clients to manipulate Entities by using REST actions.
