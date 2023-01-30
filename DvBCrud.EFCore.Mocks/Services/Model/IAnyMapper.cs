using DvBCrud.Common.Services.Mapping;
using DvBCrud.EFCore.Mocks.Core.Entities;

namespace DvBCrud.EFCore.Mocks.Services.Model;

public interface IAnyMapper : IMapper<AnyEntity, AnyModel>
{
}