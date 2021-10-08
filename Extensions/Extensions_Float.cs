using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace USCSL
{
    public static partial class Extensions
    {
        public static float Map(this float s, float min, float max, float newMin, float newMax)
        {
            return newMin + (s - min) * (newMax - newMin) / (max - min);
        }
    }
}
