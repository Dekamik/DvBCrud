using System;
using System.Reflection;
using DvBCrud.EFCore.API.Permissions;

namespace DvBCrud.EFCore.API.Extensions;

public static class CrudActionExtensions
{
    public static CrudActions GetCrudActions(this Type type) => 
        type.GetCustomAttribute<AllowedActionsAttribute>()?.AllowedActions ?? CrudActions.All;
}