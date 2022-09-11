using System;
using System.Reflection;
using DvBCrud.Common.Api.CrudActions;

namespace DvBCrud.EFCore.API.Extensions;

public static class CrudActionExtensions
{
    public static CrudAction[] GetCrudActions(this Type type) => 
        type.GetCustomAttribute<AllowedActionsAttribute>()?.AllowedActions ?? Array.Empty<CrudAction>();
}