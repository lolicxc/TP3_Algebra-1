using UnityEngine;
using System.Collections.Generic;

public sealed class BoundingBoxCalculator : MonoBehaviour
{
    // Lista para almacenar los Bounds de cada Renderer en este objeto
    public List<Bounds> ObjectBounds { get; private set; } = new List<Bounds>();

    private Renderer[] renderers;

    private void Awake()
    {
        // Obtener todos los Renderer que son hijos de este GameObject solo una vez en Awake
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        // Actualizar los Bounds en cada frame para que siempre estén actualizados
        UpdateBounds();
    }

    private void UpdateBounds()
    {
        // Asegúrate de que renderers esté inicializado
        if (renderers == null)
        {
            renderers = GetComponentsInChildren<Renderer>();
        }

        ObjectBounds.Clear();

        foreach (Renderer rend in renderers)
        {
            if (rend != null)
            {
                // Obtener los Bounds locales
                Bounds localBounds = rend.localBounds;

                // Transformar los Bounds al espacio global
                Bounds worldBounds = TransformBounds(localBounds, rend.transform);
                ObjectBounds.Add(worldBounds);
            }
        }
    }

    private Bounds TransformBounds(Bounds localBounds, Transform transform)
    {
        // Transformar el centro al espacio global
        Vector3 center = transform.TransformPoint(localBounds.center);
        Vector3 extents = localBounds.extents;
        Vector3 worldExtents = Vector3.zero;

        // Incluir la escala del objeto
        Vector3 scale = transform.lossyScale;

        // Calcular los extremos transformados en el espacio global, aplicando la escala
        Vector3[] axes = { transform.right * scale.x, transform.up * scale.y, transform.forward * scale.z };
        for (int i = 0; i < 3; i++)
        {
            // Calcular el valor absoluto de cada componente de la extensión en el espacio global con escala
            worldExtents += new Vector3(
                Mathf.Abs(axes[i].x),
                Mathf.Abs(axes[i].y),
                Mathf.Abs(axes[i].z)
            ) * extents[i];
        }

        return new Bounds(center, worldExtents * 2);
    }

    private void OnDrawGizmos()
    {
        // Asegúrate de que renderers esté inicializado antes de actualizar los Bounds
        if (renderers == null)
        {
            renderers = GetComponentsInChildren<Renderer>();
        }

        // Llamar a UpdateBounds para actualizar los Bounds mientras se dibujan los Gizmos
        UpdateBounds();

        if (ObjectBounds == null || ObjectBounds.Count == 0)
            return;

        // Dibujar cada bounding box transformada individual
        foreach (Bounds bounds in ObjectBounds)
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.1f);
            Gizmos.DrawCube(bounds.center, bounds.size);

            Gizmos.color = new Color(0f, 1f, 0f, 1f);
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }
}
