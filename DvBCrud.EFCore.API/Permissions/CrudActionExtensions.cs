﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace DvBCrud.EFCore.API.Permissions;

public static class CrudActionExtensions
{
    /// <summary>
    /// Checks whether or not the <paramref name="action"/> is allowed.
    /// </summary>
    /// <param name="allowedActions">Bitwise flag of allowed actions</param>
    /// <param name="action">Action to check for</param>
    /// <returns>True if selected action is allowed or if all actions are allowed</returns>
    public static bool IsActionAllowed(this CrudActions allowedActions, CrudActions action)
    {
        return allowedActions == CrudActions.All || (allowedActions & action) == action;
    }
    
    [ExcludeFromCodeCoverage]
    public static CrudActions GetCrudActions(this Type type) => 
        type.GetCustomAttribute<AllowedActionsAttribute>()?.AllowedActions ?? CrudActions.All;
}