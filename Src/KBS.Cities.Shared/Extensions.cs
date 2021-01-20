using System;
using System.Linq;
using System.Text;

namespace KBS.Cities.Shared
{
    public static class Extensions
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

                qs.Append($"{Uri.EscapeDataString(name)}={Uri.EscapeDataString(value)}&");
            }
            return qs.ToString();
        }
    }
}
