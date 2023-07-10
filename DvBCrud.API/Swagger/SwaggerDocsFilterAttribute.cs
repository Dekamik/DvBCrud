using System;
using System.Diagnostics.CodeAnalysis;
using DvBCrud.Shared.Permissions;

namespace DvBCrud.API.Swagger;

/// <summary>
/// Attribute for filtering out Controller methods from Swagger UI
/// </summary>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Method)]
public sealed class SwaggerDocsFilterAttribute : Attribute
{
    public CrudActions HideIfNotAllowed { get; }
    
    public SwaggerDocsFilterAttribute(CrudActions hideIfNotAllowed)
    {
        HideIfNotAllowed = hideIfNotAllowed;
    }
}