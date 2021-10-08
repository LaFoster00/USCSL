using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace USCSL
{
    public static partial class Extensions
    {
        public static Mesh DeepCopy(this Mesh m)
        {
            Mesh newMesh = new Mesh();
            newMesh.vertices = m.vertices;
            newMesh.triangles = m.triangles;
            newMesh.uv = m.uv;
            newMesh.normals = m.normals;
            newMesh.colors = m.colors;
            newMesh.tangents = m.tangents;
            return newMesh;
        }
    }
}