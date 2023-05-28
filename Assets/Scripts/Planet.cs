using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution=10;
    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    [HideInInspector]public bool shapeSettingfoldOut=true;
    [HideInInspector]public bool colorSettingfoldOut=true;
    ShapeGenerator shapeGenerator;
    [SerializeField,HideInInspector]
    MeshFilter[] meshFilters;

    TerrainFace[] terrainFace;

    void Initialize()
    {
        shapeGenerator = new ShapeGenerator(shapeSettings);
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFace = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.SetParent(transform);
                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            terrainFace[i] = new TerrainFace(shapeGenerator, meshFilters[i].mesh, resolution, directions[i]);
        }

    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    public void OnShapeSettingChanged()
    {
        Initialize();
        GenerateMesh();
    }

    public void OnColorSettingChanged()
    {
        Initialize();
        GenerateColors();
    }


    void GenerateMesh()
    {
        foreach (var item in terrainFace)
        {
            item.ConstructMesh();
        }
    }

    void GenerateColors()
    {
        foreach (var item in meshFilters)
        {
            item.GetComponent<MeshRenderer>().sharedMaterial.color = colorSettings.planetColor;
        }
    }
}
