﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2,256)]
    public int resolution = 10;
    public bool autoUpdate = true;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;

    [HideInInspector]
    public bool colorSettingsFoldout;
    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColourGenerator colorGenerator = new ColourGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
    private void OnValidate()
    {
        GeneratePlanet();
    }
    void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colorSettings);
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {


                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;
                terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            
        }
    }
    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }
    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {


            Initialize();
            GenerateMesh();
        }

    }
    public void OnColourSettingsUpdated()
    {
        if (autoUpdate)
        {


            Initialize();
            GenerateColours();
        }
    }
    void GenerateMesh()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours()
    {
        colorGenerator.UpdateColours();
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].UpdateUVs(colorGenerator);
            }
        }
    }
}
