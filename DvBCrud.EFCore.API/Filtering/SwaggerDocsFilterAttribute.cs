using System;
using DvBCrud.EFCore.API.CrudActions;

namespace DvBCrud.EFCore.API.Filtering;

[AttributeUsage(AttributeTargets.Method)]
public class SwaggerDocsFilterAttribute : Attribute
{
    public CrudAction HideIfNotAllowed { get; }
    
    public SwaggerDocsFilterAttribute(CrudAction hideIfNotAllowed)
    {
        HideIfNotAllowed = hideIfNotAllowed;
    }
}
