using System;
using System.Collections.Generic;

namespace DocDb.Mongo.Extensions
{
    internal static class PropertyInfoExtensions
    {
        public static bool IsIEnumerableType(this Type type)
        {
            return type.GetInterfaces().Contains(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }
    }
}
