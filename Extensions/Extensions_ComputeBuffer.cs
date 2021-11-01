using UnityEngine;

namespace USCSL
{
    public partial class Extensions
    {
        public static void Swap(ref ComputeBuffer buf1, ref ComputeBuffer buf2)
        {
            (buf1, buf2) = (buf2, buf1);
        }
    }
}