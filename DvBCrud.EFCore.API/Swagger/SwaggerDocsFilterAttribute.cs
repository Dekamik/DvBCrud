using System;
using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.CrudActions;

namespace DvBCrud.EFCore.API.Swagger;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Method)]
public class SwaggerDocsFilterAttribute : Attribute
{
    public CrudAction HideIfNotAllowed { get; }
    
    public SwaggerDocsFilterAttribute(CrudAction hideIfNotAllowed)
    {
        HideIfNotAllowed = hideIfNotAllowed;
    }
}
