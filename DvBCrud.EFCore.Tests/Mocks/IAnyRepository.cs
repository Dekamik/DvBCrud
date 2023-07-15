using DvBCrud.Shared;

namespace DvBCrud.EFCore.Tests.Mocks;

public interface IAnyRepository : IRepository<string, AnyModel, AnyFilter>
{
}