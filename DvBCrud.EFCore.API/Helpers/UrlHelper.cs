using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace DvBCrud.EFCore.API.Helpers;

public class UrlHelper : IUrlHelper
{
    public Uri GetResourceUrl<TId>(HttpRequest request, TId id)
    {
        return new Uri($"{request?.GetDisplayUrl() ?? "http://localhost"}/{id}");
    }
}