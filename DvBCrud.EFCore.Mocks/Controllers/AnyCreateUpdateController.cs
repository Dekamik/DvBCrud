﻿using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    [AllowedActions(CrudAction.Create, CrudAction.Update)]
    public class AnyCreateUpdateController : CrudController<string, AnyModel, IAnyCrudHandler>
    {
        public AnyCreateUpdateController(IAnyCrudHandler crudHandler) : base(crudHandler)
        {
        }
    }
}
