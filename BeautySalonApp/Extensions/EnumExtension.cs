using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        DisplayAttribute attribute = field.GetCustomAttribute<DisplayAttribute>();

        return attribute != null ? attribute.Name : value.ToString();
    }
}
