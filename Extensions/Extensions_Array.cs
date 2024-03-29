using UnityEngine;

namespace USCSL
{
    public static partial class Extensions
    {
        public static int GetRandomIndex<T>(this T[] a)
        {
            return Random.Range(0, a.Length);
        }

        public static T GetRandomValue<T>(this T[] a)
        {
            return a[Random.Range(0, a.Length)];
        }
        
        /* Return the current 2D array rotated 90° anticlockwise */
        public static T[,] Rotated<T>(this T[,] array)
        {
            int height = array.GetLength(0);
            int width = array.GetLength(1);
            T[,] result = new T[width, height];
            for (int y = 0; y < width; y++) {
                for (int x = 0; x < height; x++) {
                    result[y, x] = array[x, width - 1 - y];
                }
            }
            return result;
        }
    
        /* Return the current 2D array reflected along the x axis. */
        public static T[,] Reflected<T>(this T[,] array)
        {
            int height = array.GetLength(0);
            int width = array.GetLength(1);
            T[,] result = new T[width, height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    result[y, x] = array[y, width - 1 - x];
                }
            }
            return result;
        }
    
        public static T[,] Make2DArray<T>(this T[] input, int height, int width)
        {
            T[,] output = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            }
            return output;
        }
    }
}