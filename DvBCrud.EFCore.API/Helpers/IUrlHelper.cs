using System;
using Microsoft.AspNetCore.Http;

namespace DvBCrud.EFCore.API.Helpers;

public interface IUrlHelper
{
    Uri GetResourceUrl<TId>(HttpRequest request, TId id);
}