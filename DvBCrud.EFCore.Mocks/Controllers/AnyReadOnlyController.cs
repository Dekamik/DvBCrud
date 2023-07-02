﻿using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    [AllowedActions(CrudAction.Read)]
    public class AnyReadOnlyController : CrudController<string, AnyModel, IAnyCrudHandler>
    {
        public AnyReadOnlyController(IAnyCrudHandler crudHandler) : base(crudHandler)
        {
        }
    }
}
