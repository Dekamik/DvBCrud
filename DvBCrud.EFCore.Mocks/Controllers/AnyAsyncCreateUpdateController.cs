﻿using System.Diagnostics.CodeAnalysis;
using DvBCrud.Common.Api.CrudActions;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.API.Helpers;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    [AllowedActions(CrudAction.Create, CrudAction.Update)]
    public class AnyAsyncCreateUpdateController : AsyncCrudController<string, AnyModel, IAnyService>
    {
        public AnyAsyncCreateUpdateController(IAnyService service, IUrlHelper urlHelper) : base(service, urlHelper)
        {
        }
    }
}
