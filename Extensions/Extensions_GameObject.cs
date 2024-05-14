using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static partial class Extensions 
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetOrCreateComponent<T>(this GameObject gameObject) where T : Component
    {
        var component = gameObject.GetComponent<T>();
        return component ? component : gameObject.AddComponent<T>();
    }
}
