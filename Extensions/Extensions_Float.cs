using System;
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

        public static float NextAfter(this float from, float to)
        {
            if (float.IsNaN(from) || float.IsNaN(to)) return float.NaN;

            if (from == to) return to;

            int bits = BitConverter.SingleToInt32Bits(from);
            int direction = (from < to) ? 1 : -1;

            // If from is zero, handle it as a special case
            if (from == 0.0)
            {
                return BitConverter.Int32BitsToSingle(direction);
            }

            int nextBits = bits + direction;
            return BitConverter.Int32BitsToSingle(nextBits);
        }
        
        public static double NextAfter(this double from, double to)
        {
            if (double.IsNaN(from) || double.IsNaN(to)) return double.NaN;

            if (from == to) return to;

            long bits = BitConverter.DoubleToInt64Bits(from);
            long direction = (from < to) ? 1 : -1;

            // If from is zero, handle it as a special case
            if (from == 0.0)
            {
                return BitConverter.Int64BitsToDouble(direction);
            }

            long nextBits = bits + direction;
            return BitConverter.Int64BitsToDouble(nextBits);
        }
    }
}