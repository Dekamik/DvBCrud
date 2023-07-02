using System;

namespace DvBCrud.EFCore.Entities;

public interface IModifiedAt
{
    public DateTimeOffset ModifiedAt { get; set; }
}