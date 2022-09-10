using System;
using DvBCrud.Common.Api.Swagger;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DvBCrud.EFCore.API.Extensions;

public static class AspNetCoreExtensions
{
    public static IServiceCollection AddCrudSwaggerGen(this IServiceCollection services, Action<SwaggerGenOptions>? setupAction = null)
    {
        services.AddSwaggerGen(c =>
        {
            c.DocumentFilter<SwaggerDocsFilter>();
            setupAction?.Invoke(c);
        });

        return services;
    }
}