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
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            public AtomicSafetyHandle safetyHandle;

            public NativeArrayRef(IntPtr ptr, int length, AtomicSafetyHandle safetyHandle)
            {
                this.ptr = ptr;
                this.length = length;
                this.safetyHandle = safetyHandle;
            }
#else
            public NativeArrayRef(IntPtr ptr, int length)
            {
                this.ptr = ptr;
                this.length = length;
            }
#endif
        }

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        /// <summary>
        /// Not Burst Compatible!
        /// </summary>
        /// <param name="array"></param>
        /// <param name="handle"></param>
        /// <typeparam name="T"></typeparam>
        [BurstDiscard]
        public static unsafe void GetSafetyHandle<T>(ref NativeArray<T> array, out AtomicSafetyHandle handle)
            where T : struct
        {
            handle = NativeArrayUnsafeUtility.GetAtomicSafetyHandle(array);
        }
#endif

        /// <summary>
        /// Not Burst Compatible!
        /// </summary>
        /// <param name="array"></param>
        /// <param name="handle"></param>
        /// <typeparam name="T"></typeparam>
        [BurstDiscard]
        public static void GetReadWriteRef<T>(ref NativeArray<T> array, out NativeArrayRef @ref) where T : struct
        {
            GetReadWriteIntPtr(ref array, out var ptr);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            GetSafetyHandle(ref array, out var handle);
            @ref = new NativeArrayRef(ptr, array.Length, handle);
#else
            @ref = new NativeArrayRef(ptr, array.Length);
#endif
        }
        
        /// <summary>
        /// Not Burst Compatible!
        /// </summary>
        /// <param name="array"></param>
        /// <param name="handle"></param>
        /// <typeparam name="T"></typeparam>
        [BurstDiscard]
        public static void GetReadonlyRef<T>(ref NativeArray<T> array, out NativeArrayRef @ref) where T : struct
        {
            GetReadonlyIntPtr(ref array, out var ptr);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            GetSafetyHandle(ref array, out var handle);
            @ref = new NativeArrayRef(ptr, array.Length, handle);
#else
            @ref = new NativeArrayRef(ptr, array.Length);
#endif
        }

        /// <summary>
        /// Not Burst Compatible!
        /// </summary>
        /// <param name="array"></param>
        /// <param name="handle"></param>
        /// <typeparam name="T"></typeparam>
        [BurstDiscard]
        public static unsafe void ToNativeArray<T>(ref NativeArrayRef arrayRef, out NativeArray<T> array)
            where T : struct
        {
            NativeArray<T> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(arrayRef.ptr.ToPointer(),
                arrayRef.length, Allocator.None);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref nativeArray, arrayRef.safetyHandle);
#endif
            array = nativeArray;
        }

        /// <summary>
        /// Not Burst Compatible!
        /// </summary>
        /// <param name="array"></param>
        /// <param name="handle"></param>
        /// <typeparam name="T"></typeparam>
        [BurstDiscard]
        public static unsafe void ToNativeReadonlyArray<T>(ref NativeArrayRef arrayRef,
            out NativeArray<T>.ReadOnly array) where T : struct
        {
            ToNativeArray<T>(ref arrayRef, out var readWriteArray);
            array = readWriteArray.AsReadOnly();
        }

        /// <summary>
        /// Not Burst Compatible!
        /// </summary>
        /// <param name="array"></param>
        /// <param name="handle"></param>
        /// <typeparam name="T"></typeparam>
        [BurstDiscard]
        public static unsafe void GetReadWriteIntPtr<T>(ref NativeArray<T> array, out IntPtr ptr) where T : struct
        {
            ptr = new IntPtr(array.GetUnsafePtr());
        }

        /// <summary>
        /// Not Burst Compatible!
        /// </summary>
        /// <param name="array"></param>
        /// <param name="handle"></param>
        /// <typeparam name="T"></typeparam>
        [BurstDiscard]
        public static unsafe void GetReadonlyIntPtr<T>(ref NativeArray<T> array, out IntPtr ptr) where T : struct
        {
            ptr = new IntPtr(array.GetUnsafeReadOnlyPtr());
        }

        [BurstCompile]
        public static int SumInts(ref NativeSlice<int> array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }
        
        [BurstCompile]
        public static double SumDoubles(ref NativeSlice<double> array)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }
    }
}