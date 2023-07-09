﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace DvBCrud.API.Permissions;

/// <summary>
/// Defines which actions are allowed on a CrudController
/// </summary>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class)]
public sealed class AllowedActionsAttribute : Attribute
{
    public CrudActions AllowedActions { get; }

    public AllowedActionsAttribute(CrudActions allowedActions)
    {
        AllowedActions = allowedActions;
    }
}