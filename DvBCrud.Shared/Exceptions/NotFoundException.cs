using System.Diagnostics.CodeAnalysis;

namespace DvBCrud.Shared.Exceptions;

[ExcludeFromCodeCoverage]
public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    // ReSharper disable once UnusedMember.Global
    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}