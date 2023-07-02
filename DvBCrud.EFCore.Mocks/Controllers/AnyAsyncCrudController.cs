using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.Mocks.Core.Repositories;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    public class AnyAsyncCrudController : AsyncCrudController<string, AnyModel, IAnyRepository>
    {
        public AnyAsyncCrudController(IAnyRepository anyCrudHandler) : base(anyCrudHandler)
        {

        }
    }
}
