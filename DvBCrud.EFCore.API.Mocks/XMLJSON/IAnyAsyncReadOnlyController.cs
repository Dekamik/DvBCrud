using DvBCrud.EFCore.API.XMLJSON;
using DvBCrud.EFCore.Mocks.Entities;

namespace DvBCrud.EFCore.API.Mocks.XMLJSON
{
    public interface IAnyAsyncReadOnlyController : IAsyncReadOnlyController<AnyEntity, int>
    {
    }
}
