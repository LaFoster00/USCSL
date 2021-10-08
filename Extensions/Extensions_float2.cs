using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace USCSL
{
    public static partial class Extensions
    {
        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            degrees *= -1;
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }

        public static float Angle(this Vector2 v, Vector2 to = default)
        {
            return Vector2.SignedAngle(v, to);
        }

        public static Vector2Int FloorToVector2Int(this Vector2 v)
        {
            Vector2Int flooredVector = new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
            return flooredVector;
        }
    }
}