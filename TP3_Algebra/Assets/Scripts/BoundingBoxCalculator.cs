using UnityEngine;
using System.Collections.Generic;

public sealed class BoundingBoxCalculator : MonoBehaviour
{
    // Lista para almacenar los Bounds de cada Renderer en este objeto
    public List<Bounds> ObjectBounds { get; private set; } = new List<Bounds>();

    private void Awake()
    {
        // Obtener todos los Renderer que son hijos de este GameObject
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        ObjectBounds.Clear();

        foreach (Renderer rend in renderers)
        {
            if (rend != null)
            {
                // Agregar el Bounds de cada Renderer a la lista
                ObjectBounds.Add(rend.bounds);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (ObjectBounds == null || ObjectBounds.Count == 0)
            return;

        // Dibujar cada bounding box individual
        foreach (Bounds bounds in ObjectBounds)
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.1f);
            Gizmos.DrawCube(bounds.center, bounds.size);

            Gizmos.color = new Color(0f, 1f, 0f, 1f);
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }
}