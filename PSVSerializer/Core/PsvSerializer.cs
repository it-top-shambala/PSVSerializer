using System.Reflection;
using System.Text;
using PSVSerializer.Attributes;

namespace PSVSerializer.Core;

public static class PsvSerializer
{
    private static readonly StringBuilder StringBuilder = new();

    private static void RemoveLastDelimiter()
    {
        StringBuilder.Remove(StringBuilder.Length - 1, 1);
    }
    private static string GetPropertyNames(IEnumerable<PropertyInfo> properties)
    {
        StringBuilder.Clear();
        
        foreach (var property in properties)
        {
            var ignoreAttribute = property.GetCustomAttribute<PsvIgnoreAttribute>();
            if (ignoreAttribute is not null) continue;

            var nameAttribute = property.GetCustomAttribute<PsvNameAttribute>();
            if (nameAttribute is null)
            {
                StringBuilder.Append(property.Name);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(nameAttribute.Name))
                {
                    throw new CustomAttributeFormatException($"{nameof(PsvNameAttribute)} указан с пустым значением {nameof(PsvNameAttribute.Name)}");
                }
                StringBuilder.Append(nameAttribute.Name);
            }
            StringBuilder.Append('|');
        }

        RemoveLastDelimiter();
        return StringBuilder.ToString();
    }
    private static string GetPropertyValues(IEnumerable<PropertyInfo> properties, object obj)
    {
        StringBuilder.Clear();
        
        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttribute<PsvIgnoreAttribute>();
            if (attribute is not null) continue;
            
            StringBuilder.Append(property.GetValue(obj));
            StringBuilder.Append('|');
        }

        RemoveLastDelimiter();
        return StringBuilder.ToString();
    }
    public static string Serialize(object obj)
    {
        var type = obj.GetType();
        var properties = type.GetProperties();
        return $"""
                {GetPropertyNames(properties)}
                {GetPropertyValues(properties, obj)}
                """;
    }
}