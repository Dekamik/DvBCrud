using System.Diagnostics.CodeAnalysis;
using DvBCrud.API.Permissions;
using DvBCrud.Shared;

namespace DvBCrud.API.Tests.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    [AllowedActions(CrudActions.Read)]
    public class AnyReadOnlyController : CrudController<string, AnyModel, IRepository<string, AnyModel>>
    {
        public AnyReadOnlyController(IRepository<string, AnyModel> repository) : base(repository)
        {
        }
    }
}
