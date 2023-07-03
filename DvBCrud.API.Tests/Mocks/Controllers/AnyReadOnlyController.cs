﻿using System.Diagnostics.CodeAnalysis;
using DvBCrud.API.Controllers;
using DvBCrud.API.Permissions;
using DvBCrud.Shared;

namespace DvBCrud.API.Tests.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    [AllowedActions(CrudActions.Read)]
    public class AnyReadOnlyController : CrudController<string, AnyModel, IRepository<string, AnyModel>>
    {
        public AnyReadOnlyController(IRepository<string, AnyModel> crudHandler) : base(crudHandler)
        {
        }
    }
}
