using System;
using System.Collections.Generic;
using System.Linq;

namespace ExLibris
{
    public static class Common
    {
        public static bool In<T>(this T o, params T[] set) where T : IEquatable<T>
        {
            return set.Any(item => item.Equals(o));
        }

        public static bool In<T>(this T o, IEnumerable<T> set) where T : IEquatable<T>
        {
            return set.Any(item => item.Equals(o));
        }
    }
}