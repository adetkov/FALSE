using System;
using System.Collections.Generic;

namespace ExLibris
{
    public static class Common
    {
        public static bool into<T>(this T o, params T[] set) where T : IEquatable<T>
        {
            foreach (var item in set)
                if (item.Equals(o)) return true;

            return false;
        }

        public static bool into<T>(this T o, IEnumerable<T> set) where T : IEquatable<T>
        {
            foreach (var item in set)
                if (item.Equals(o)) return true;

            return false;
        }

        public static IEnumerable<int> to(this int from, int to)
        {
            for (; from < to; from++)
                yield return from;
        }
    }
}