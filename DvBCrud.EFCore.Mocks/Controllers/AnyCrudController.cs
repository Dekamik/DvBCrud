using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.Mocks.Core.Repositories;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    public class AnyCrudController : CrudController<string, AnyModel, IAnyRepository>
    {
        public AnyCrudController(IAnyRepository crudHandler) : base(crudHandler)
        {
        }
    }
}
