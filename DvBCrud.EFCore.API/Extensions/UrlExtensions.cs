using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace DvBCrud.EFCore.API.Extensions;

public static class UrlExtensions
{
    public static Uri GetResourceUrl<TId>(this HttpRequest request, TId id)
    {
        // Backup URL is mainly for unit tests
        return new Uri($"{request?.GetDisplayUrl() ?? "http://localhost"}/{id}");
    }
}