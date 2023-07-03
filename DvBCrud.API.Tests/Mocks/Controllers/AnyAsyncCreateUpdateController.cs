using System.Diagnostics.CodeAnalysis;
using DvBCrud.API.Controllers;
using DvBCrud.API.Permissions;
using DvBCrud.Shared;

namespace DvBCrud.API.Tests.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    [AllowedActions(CrudActions.Create | CrudActions.Update)]
    public class AnyAsyncCreateUpdateController : AsyncCrudController<string, AnyModel, IRepository<string, AnyModel>>
    {
        public AnyAsyncCreateUpdateController(IRepository<string, AnyModel> repository) : base(repository)
        {
        }
    }
}
