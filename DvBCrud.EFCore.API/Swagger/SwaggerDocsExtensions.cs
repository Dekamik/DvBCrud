using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DvBCrud.EFCore.API.Swagger;

public static class SwaggerDocsExtensions
{
    [ExcludeFromCodeCoverage]
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