using DvBCrud.Shared;

namespace DvBCrud.Tests.UnitTests.EFCore.Mocks;

public interface IAnyRepository : IRepository<string, AnyModel, AnyFilter>
{
}