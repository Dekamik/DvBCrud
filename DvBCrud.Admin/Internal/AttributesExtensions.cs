using System.Reflection;

namespace DvBCrud.Admin.Internal;

public static class AttributesExtensions
{
    public static T? GetAttribute<T>(this MemberInfo member) 
        where T : Attribute
    {
        return member.GetCustomAttributes<T>().SingleOrDefault();
    }
}