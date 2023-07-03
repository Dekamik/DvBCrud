using System.Diagnostics.CodeAnalysis;
using DvBCrud.API.Controllers;
using DvBCrud.Shared;

namespace DvBCrud.API.Tests.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    public class AnyCrudController : CrudController<string, AnyModel, IRepository<string, AnyModel>>
    {
        public AnyCrudController(IRepository<string, AnyModel> crudHandler) : base(crudHandler)
        {
        }
    }
}
