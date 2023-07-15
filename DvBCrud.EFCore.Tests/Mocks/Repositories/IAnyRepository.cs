using DvBCrud.Shared;

namespace DvBCrud.EFCore.Tests.Mocks.Repositories;

public interface IAnyRepository : IRepository<string, AnyModel, AnyFilter>
{
}