using System.Collections.Generic;
using UnityEngine;

namespace USCSL
{
    public static partial class Extensions
    {
        public static int GetRandomIndex<T>(this List<T> a)
        {
            return Random.Range(0, a.Count);
        }

        public static T GetRandomValue<T>(this List<T> a)
        {
            return a[Random.Range(0, a.Count)];
        }
    }
}