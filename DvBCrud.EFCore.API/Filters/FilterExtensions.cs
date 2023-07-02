using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.EFCore.API.Filters;

[ExcludeFromCodeCoverage]
public static class FilterExtensions
{
    public static void AddCrudExceptionFilters(this MvcOptions options)
    {
        options.Filters.Add<NotFoundExceptionFilterAttribute>();
    }
}