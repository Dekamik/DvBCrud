using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace DvBCrud.EFCore.API.Helpers;

public static class UrlHelper
{
    public static Uri GetResourceUrl<TId>(HttpRequest request, TId id)
    {
        return new Uri($"{request.GetDisplayUrl()}/{id}");
    }
}