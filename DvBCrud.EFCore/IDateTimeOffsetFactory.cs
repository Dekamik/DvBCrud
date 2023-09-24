using System;

namespace DvBCrud.EFCore;

public interface IDateTimeOffsetFactory
{
    DateTimeOffset UtcNow();
}