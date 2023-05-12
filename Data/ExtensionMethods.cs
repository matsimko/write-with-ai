using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data;
internal static class ExtensionMethods
{
    public static void ShallowCopyPropertiesTo<T>(this T from, T to) where T : notnull
    {
        foreach (var prop in typeof(T).GetProperties())
        {
            if (prop.CanRead && prop.CanWrite)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                object value = prop.GetValue(from);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                prop.SetValue(to, value);
            }

        }
    }
}
