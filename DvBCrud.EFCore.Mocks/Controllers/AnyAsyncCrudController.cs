using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    public class AnyAsyncCrudController : AsyncCrudController<string, AnyModel, IAnyCrudHandler>
    {
        public AnyAsyncCrudController(IAnyCrudHandler anyCrudHandler) : base(anyCrudHandler)
        {

        }
    }
}
