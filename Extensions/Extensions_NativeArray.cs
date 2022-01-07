using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace USCSL
{
    [BurstCompile]
    public partial class Extensions
    {
        public struct NativeArrayRef
        {
            public IntPtr ptr;
            public int length;

            public NativeArrayRef(IntPtr ptr, int length)
            {
                this.ptr = ptr;
                this.length = length;
            }
        }
        
        [BurstDiscard]
        public static void GetRef<T>(ref NativeArray<T> array, out NativeArrayRef @ref) where T : struct
        {
            GetIntPtr(ref array, out var ptr);
            @ref = new NativeArrayRef(ptr, array.Length);
        }

        [BurstDiscard]
        public static unsafe void ToArray<T>(ref NativeArrayRef arrayRef, Allocator allocator, out NativeArray<T> array)
            where T : struct
        {
            array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(arrayRef.ptr.ToPointer(),
                arrayRef.length, allocator);
        }

        [BurstDiscard]
        public static unsafe void GetIntPtr<T>(ref NativeArray<T> array, out IntPtr ptr) where T : struct
        {
            ptr = new IntPtr(array.GetUnsafePtr());
        }

        [BurstCompile]
        public static int SumInts(ref NativeArrayRef arrayRef)
        {
            ToArray<int>(ref arrayRef, Allocator.Temp, out var array);
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }
        
        [BurstCompile]
        public static double SumDoubles(ref NativeArrayRef arrayRef)
        { 
            ToArray<double>(ref arrayRef, Allocator.Temp, out var array);
            double sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }
    }
}