using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridGenerator : MonoBehaviour
{
    public int gridSize = 10;
    public float spacing = 1f;
    public List<Vector3> gridPoints = new List<Vector3>();

    void Start()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    Vector3 point = new Vector3(x * spacing, y * spacing, z * spacing);
                    gridPoints.Add(point);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector3 point in gridPoints)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}