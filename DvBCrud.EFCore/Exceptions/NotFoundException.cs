using System;
using System.Diagnostics.CodeAnalysis;

namespace DvBCrud.EFCore.Exceptions;

[ExcludeFromCodeCoverage]
public class NotFoundException : Exception
{
    public NotFoundException() : base()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerExeption) : base(message, innerExeption)
    {
    }
}