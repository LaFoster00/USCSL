using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace USCSL
{
    public static partial class Extensions
    {
        /* Mirrors a texture along the x axis */
        public static Texture2D Mirrored(this Texture2D original)
        {
            int width = original.width;
            int height = original.height;
            Color[] pixels = new Color[width * height];
        
            int xN = width;
            int yN = height;

            for (int x = width - 1; x >= 0; x--)
            {
                for (int y = 0; y < height; y++)
                {
                    pixels[x + y * width] = original.GetPixel(width - 1 - x, y, 0);
                }
            }
            
            Texture2D mirrored = new Texture2D(original.width, original.height, TextureFormat.RGBA32, original.mipmapCount != 0)
                {
                    filterMode = original.filterMode
                };
            mirrored.SetPixels(pixels);
            mirrored.Apply();
         
            return mirrored;
        }
    
        /* Rotates a texture 90 degrees counter clockwise */
        public static Texture2D Rotated(this Texture2D original)
        {
            Color[,] pixels = original.GetPixels().Make2DArray(original.height, original.width);
            int height = original.height;
            int width = original.width;
            Color[,] rotatedPixels = new Color[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    rotatedPixels[y, x] = pixels[width - 1 - x, y];
                }
            }
            
            Texture2D result = new Texture2D(original.width, original.height, TextureFormat.RGBA32, original.mipmapCount != 0)
                {
                    filterMode = original.filterMode
                };
            result.SetPixels(rotatedPixels.Cast<Color>().ToArray());
            result.Apply();
            return result;
        }
    }
}