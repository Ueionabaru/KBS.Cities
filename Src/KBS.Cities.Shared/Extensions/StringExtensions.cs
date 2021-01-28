using System;
using System.Linq;
using System.Text;

namespace KBS.Cities.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string ToQueryString(this object obj)
        {
            var qs = new StringBuilder("?");

            var objType = obj.GetType();

            var properties = objType.GetProperties()
                .Where(p => p.GetValue(obj, null) != null);

            foreach (var prop in properties)
            {
                var name = prop.Name;
                var value = prop.GetValue(obj)?
                    .ToString();

                // Variant date formats is a terrible thing...
                if (prop.PropertyType == typeof(DateTime))
                    value = $"{prop.GetValue(obj):MM/dd/yyyy}";
                
                qs.Append($"{Uri.EscapeDataString(name)}={Uri.EscapeDataString(value)}&");
            }

            qs.Length--;

            return qs.ToString();
        }
    }
}
