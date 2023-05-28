using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    public ShapeSettings shapeSettings;

    public ShapeGenerator(ShapeSettings shapeSettings)
    {
        this.shapeSettings = shapeSettings;
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnSphere)
    {
        return pointOnSphere * shapeSettings.radius;
    }
}
