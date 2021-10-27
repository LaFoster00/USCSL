using UnityEngine;

namespace USCSL
{
    public static class Extensions_Texture2DArray
    {
        public static void Swap(ref Texture2DArray tex1, ref Texture2DArray tex2)
        {
            (tex1, tex2) = (tex2, tex1);
        }
    }
}