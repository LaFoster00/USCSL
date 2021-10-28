using UnityEngine;

namespace USCSL
{
    public static partial class Extensions
    {
        public static void Swap(ref Texture3D tex1, ref Texture3D tex2)
        {
            (tex1, tex2) = (tex2, tex1);
        }
    }
}