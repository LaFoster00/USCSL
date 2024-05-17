using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampAttribute : PropertyAttribute
{
    public string MinFieldName { get; }
    public string MaxFieldName { get; }

    public ClampAttribute(string minFieldName, string maxFieldName)
    {
        MinFieldName = minFieldName;
        MaxFieldName = maxFieldName;
    }
}
