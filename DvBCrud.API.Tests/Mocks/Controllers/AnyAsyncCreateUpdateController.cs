﻿using System.Diagnostics.CodeAnalysis;
using DvBCrud.API.Permissions;
using DvBCrud.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.API.Tests.Mocks.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
[Route("api/v1/[controller]")]
[AllowedActions(CrudActions.Create | CrudActions.Update)]
public class AnyAsyncCreateUpdateController : AsyncCrudController<string, AnyModel, IRepository<string, AnyModel>>
{
    public AnyAsyncCreateUpdateController(IRepository<string, AnyModel> repository) : base(repository)
    {
    }
}