using System;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DvBCrud.API.Swagger;

public static class SwaggerDocsExtensions
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