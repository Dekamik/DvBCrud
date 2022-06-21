using System;

namespace DvBCrud.EFCore.API.CrudActions;

[AttributeUsage(AttributeTargets.Class)]
public class CrudActionAttribute : Attribute
{
    public CrudAction[] AllowedActions { get; }

    public CrudActionAttribute(params CrudAction[] allowedActions)
    {
        AllowedActions = allowedActions;
    }
}
