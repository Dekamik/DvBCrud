using DvBCrud.EFCore.API.JSON;
using DvBCrud.EFCore.Mocks.Entities;

namespace DvBCrud.EFCore.API.Mocks.JSONControllers
{
    public interface IAnyReadOnlyController : IReadOnlyController<AnyEntity, int>
    {
    }
}
