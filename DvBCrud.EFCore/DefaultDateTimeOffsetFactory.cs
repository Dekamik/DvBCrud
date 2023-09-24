using System;

namespace DvBCrud.EFCore;

public class DefaultDateTimeOffsetFactory : IDateTimeOffsetFactory
{
    public DateTimeOffset UtcNow() => DateTimeOffset.Now;
}