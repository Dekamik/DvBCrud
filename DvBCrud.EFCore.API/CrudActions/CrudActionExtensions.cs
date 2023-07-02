using System.Collections.Generic;
using System.Linq;

namespace DvBCrud.EFCore.API.CrudActions;

public static class CrudActionExtensions
{
    /// <summary>
    /// Checks whether or not the <paramref name="action"/> is allowed.
    ///
    /// The <paramref name="action"/> is allowed if either
    /// A: it is found in <paramref name="allowedActions"/> or
    /// B: <paramref name="allowedActions"/> is null or empty. 
    /// </summary>
    /// <param name="allowedActions">An array of allowed actions</param>
    /// <param name="action">Action to check for</param>
    /// <returns>True if selected action is allowed or if <paramref name="allowedActions"/> is null or empty</returns>
    public static bool IsActionAllowed(this IEnumerable<CrudAction>? allowedActions, CrudAction action)
    {
        var allowedActionsArr = allowedActions?.ToArray();
        return allowedActionsArr == null ||
               !allowedActionsArr.Any() ||
               allowedActionsArr.Contains(action);
    }
}