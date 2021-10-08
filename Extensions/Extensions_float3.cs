using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace USCSL
{
    public static partial class Extensions
    {
        public static float3 MultiplyComponents(this float3 v1, float3 v2)
        {
            return new float3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        public static float GetXZDistance(float3 v1, float3 v2)
        {
            return math.distance(v1.MultiplyComponents(new float3(1, 0, 1)), v2.MultiplyComponents(new float3(1, 0, 1)));
        }

        public static float GetXZMagnitude(this float3 v1)
        {
            return math.length(v1.MultiplyComponents(new float3(1, 0, 1)));
        }

        public static float3 GetRandomPosition(float range)
        {
            return new float3(Random.Range(-range, range), 0, Random.Range(-range, range));
        }

        public static float3 GetRandomPosition(float2 range)
        {
            return new float3(Random.Range(-range.x, range.x), 0, Random.Range(-range.y, range.y));
        }
    }
}