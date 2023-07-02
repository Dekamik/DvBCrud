using System;
using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.CrudActions;

namespace DvBCrud.EFCore.API.Swagger;

/// <summary>
/// Attribute for filtering out Controller methods from Swagger UI
/// </summary>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Method)]
public sealed class SwaggerDocsFilterAttribute : Attribute
{
    public CrudAction HideIfNotAllowed { get; }
    
    public SwaggerDocsFilterAttribute(CrudAction hideIfNotAllowed)
    {
        HideIfNotAllowed = hideIfNotAllowed;
    }
}