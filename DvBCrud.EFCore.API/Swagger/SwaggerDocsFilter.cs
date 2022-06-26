using System.Reflection;
using DvBCrud.EFCore.API.CrudActions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DvBCrud.EFCore.API.Swagger;

public class SwaggerDocsFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var description in context.ApiDescriptions)
        {
            var actionDescriptor = (ControllerActionDescriptor)description.ActionDescriptor;
            var action = actionDescriptor.MethodInfo.GetCustomAttribute<SwaggerDocsFilterAttribute>()?.HideIfNotAllowed;
            var actionsAttribute = actionDescriptor.ControllerTypeInfo.GetCustomAttribute<AllowedActionsAttribute>();
            
            if (action == null || actionsAttribute == null)
                continue;

            if (actionsAttribute.AllowedActions.IsActionAllowed(action.Value)) 
                continue;
            
            var method = description.HttpMethod!.ToLower();

            foreach (var path in swaggerDoc.Paths)
            {
                foreach (var operation in path.Value.Operations.Keys)
                {
                    switch (operation)
                    {
                        case OperationType.Get:
                            if (method == "get")
                                path.Value.Operations.Remove(operation);
                            break;
                        
                        case OperationType.Post:
                            if (method == "post")
                                path.Value.Operations.Remove(operation);
                            break;
                        
                        case OperationType.Put:
                            if (method == "put")
                                path.Value.Operations.Remove(operation);
                            break;
                        
                        case OperationType.Delete:
                            if (method == "delete")
                                path.Value.Operations.Remove(operation);
                            break;

                        case OperationType.Options:
                        case OperationType.Head:
                        case OperationType.Patch:
                        case OperationType.Trace:
                        default:
                            break;
                    }
                }
            }
        }
    }
}
