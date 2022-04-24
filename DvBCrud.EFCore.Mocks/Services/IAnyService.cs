using DvBCrud.EFCore.Mocks.Services.Model;
using DvBCrud.EFCore.Services;

namespace DvBCrud.EFCore.Mocks.Services;

public interface IAnyService : IService<string, AnyModel>
{
}