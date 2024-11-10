using UnityEngine;
using System.Collections.Generic;

public class CollisionChecker : MonoBehaviour
{
    private List<BoundingBoxCalculator> BoundingBoxCalculator;

    public GridGenerator grid; // Asigna esto en el Inspector si es necesario

    void Start()
    {
        // Encuentra todos los objetos en la escena con el script BoundingBoxCalculator
        BoundingBoxCalculator = new List<BoundingBoxCalculator>(FindObjectsOfType<BoundingBoxCalculator>());
    }

    void Update()
    {
        // Verificar colisiones entre todos los pares de bounding boxes individuales
        for (int i = 0; i < BoundingBoxCalculator.Count; i++)
        {
            for (int j = i + 1; j < BoundingBoxCalculator.Count; j++)
            {
                // Obtener las listas de Bounds de los dos objetos
                List<Bounds> boundsList1 = BoundingBoxCalculator[i].ObjectBounds;
                List<Bounds> boundsList2 = BoundingBoxCalculator[j].ObjectBounds;

                // Verificar cada combinación de Bounds entre los dos objetos
                foreach (Bounds bounds1 in boundsList1)
                {
                    foreach (Bounds bounds2 in boundsList2)
                    {
                        if (bounds1.Intersects(bounds2))
                        {
                            CheckGridPointsCollision(bounds1, bounds2);
                        }
                    }
                }
            }
        }
    }

    void CheckGridPointsCollision(Bounds bounds1, Bounds bounds2)
    {
        foreach (Vector3 point in grid.gridPoints)
        {
            if (bounds1.Contains(point) && bounds2.Contains(point))
            {
                Debug.Log("Colisión detectada en el punto: " + point);
                return;
            }
        }
        Debug.Log("No hay colisión en los puntos de la grilla entre estos objetos.");
    }
}