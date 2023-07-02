using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.API.Permissions;
using DvBCrud.EFCore.Mocks.Core.Repositories;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    [AllowedActions(CrudActions.Read)]
    public class AnyReadOnlyController : CrudController<string, AnyModel, IAnyRepository>
    {
        public AnyReadOnlyController(IAnyRepository crudHandler) : base(crudHandler)
        {
        }
    }
}
