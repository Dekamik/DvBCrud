using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.API.Permissions;
using DvBCrud.EFCore.Mocks.Core.Repositories;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    [AllowedActions(CrudActions.Create | CrudActions.Update)]
    public class AnyCreateUpdateController : CrudController<string, AnyModel, IAnyRepository>
    {
        public AnyCreateUpdateController(IAnyRepository repository) : base(repository)
        {
        }
    }
}
