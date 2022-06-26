using System;

namespace DvBCrud.EFCore.API.CrudActions;

[AttributeUsage(AttributeTargets.Class)]
public class AllowedActionsAttribute : Attribute
{
    public CrudAction[] AllowedActions { get; }

    public AllowedActionsAttribute(params CrudAction[] allowedActions)
    {
        AllowedActions = allowedActions;
    }
}
