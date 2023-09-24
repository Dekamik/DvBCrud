using DvBCrud.EFCore;

namespace DvBCrud.Tests.IntegrationTests;

public class MockDateTimeOffsetFactory : IDateTimeOffsetFactory
{
    public DateTimeOffset MockUtcNow { get; set; }

    public DateTimeOffset UtcNow() => MockUtcNow;
}