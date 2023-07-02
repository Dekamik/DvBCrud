using DvBCrud.EFCore.API.Handlers;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Services;

public interface IAnyCrudHandler : ICrudHandler<string, AnyModel>
{
}