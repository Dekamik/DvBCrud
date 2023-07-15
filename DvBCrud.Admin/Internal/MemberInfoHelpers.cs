using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DvBCrud.Admin.Internal.Wrappers;

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

    public static Dictionary<string, Wrapper<dynamic>> ToPropertyWrappers<T>(this T item)
    {
        var properties = GetPropertyInfos<T>();
        return properties.ToDictionary(prop => prop.Name, prop => new Wrapper<dynamic>(prop.GetValue(item)));
    }

    public static void FromPropertyWrappers<T>(this T item, Dictionary<string, Wrapper<dynamic>> wrappers)
    {
        var properties = GetPropertyInfos<T>();

        foreach (var prop in properties)
        {
            if (!prop.CanWrite || !(prop.GetAttribute<DisplayAttribute>()?.GetAutoGenerateField() ?? true))
            {
                continue;
            }
            prop.SetValue(item, wrappers[prop.Name].Value);
        }
    }
}