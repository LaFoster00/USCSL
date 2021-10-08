using System;
using UnityEngine;

namespace USCSL
{
    public static partial class Extensions
    {
        public static bool FindGround(this CharacterController c, out RaycastHit hitResult, float maxDistance = Single.MaxValue)
        {
            return Physics.CapsuleCast(new Vector3(0, c.radius, 0), new Vector3(0, c.height - c.radius, 0), c.radius,
                Vector3.down, out hitResult, maxDistance);
        }
    }
}