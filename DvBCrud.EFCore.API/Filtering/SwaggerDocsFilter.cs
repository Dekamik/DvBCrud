using System;
using System.Linq;
using System.Reflection;
using DvBCrud.EFCore.API.CrudActions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DvBCrud.EFCore.API.Filtering;

public class SwaggerDocsFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var description in context.ApiDescriptions)
        {
            var actionDescriptor = (ControllerActionDescriptor)description.ActionDescriptor;
            var action = actionDescriptor.MethodInfo.GetCustomAttribute<SwaggerDocsFilterAttribute>()?.HideIfNotAllowed;
            var actionsAttribute = actionDescriptor.ControllerTypeInfo.GetCustomAttribute<CrudActionAttribute>();
            
            if (action == null || actionsAttribute == null)
                continue;

            if (actionsAttribute.AllowedActions.IsActionAllowed(action.Value)) 
                continue;
            
            var method = description.HttpMethod;
            
            
            
            var operations = swaggerDoc.Paths.Select(k => string.Join("\nKey: ", k.Value.Operations.Keys));
            Console.WriteLine(method);
            Console.WriteLine(operations);
        }
    }
}
