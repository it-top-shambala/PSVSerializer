using System.Reflection;
using System.Text;
using PSVSerializer.Attributes;

namespace PSVSerializer.Core;

public static class PsvSerializer
{
    public static string Serialize(object obj)
    {
        var stringBuilder = new StringBuilder();
        
        var type = obj.GetType();
        var properties = type.GetProperties();
        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttribute<PsvIgnoreAttribute>();
            if (attribute is not null)
            {
                continue;
            }

            stringBuilder.Append(property.GetValue(obj));
            stringBuilder.Append('|');
        }

        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        return stringBuilder.ToString();
    }
}