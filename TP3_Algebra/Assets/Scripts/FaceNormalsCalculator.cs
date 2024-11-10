using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FaceNormalsCalculator : MonoBehaviour
{
    void CalculateFaceNormals()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v1 = vertices[triangles[i]];
            Vector3 v2 = vertices[triangles[i + 1]];
            Vector3 v3 = vertices[triangles[i + 2]];

            Vector3 normal = Vector3.Cross(v2 - v1, v3 - v1).normalized;

            // Asegurarse que la normal apunte al centro del modelo
            Vector3 faceCenter = (v1 + v2 + v3) / 3;
            if (Vector3.Dot(normal, faceCenter - transform.position) < 0)
            {
                normal = -normal;
            }
        }
    }
}