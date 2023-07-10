using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DvBCrud.Admin.Internal;

public static class MemberInfoHelpers
{
    public static T? GetAttribute<T>(this MemberInfo member) 
        where T : Attribute
    {
        return member.GetCustomAttributes<T>().SingleOrDefault();
    }
    
    public static PropertyInfo[] GetPropertyInfos<T>()
    {
        return typeof(T).GetProperties()
            .OrderBy(p => p.GetAttribute<DisplayAttribute>()?.GetOrder())
            .ToArray();
    }

    public static string? GetDisplayName(this MemberInfo prop)
    {
        var attr = prop.GetAttribute<DisplayAttribute>();
        return attr != null ? attr.Name ?? prop.Name : prop.Name;
    }
}