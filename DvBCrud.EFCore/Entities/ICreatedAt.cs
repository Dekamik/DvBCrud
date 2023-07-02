using System;

namespace DvBCrud.EFCore.Entities;

public interface ICreatedAt
{
    public DateTimeOffset CreatedAt { get; set; }
}