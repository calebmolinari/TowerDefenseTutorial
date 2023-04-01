using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SloppyTerrainGenerator : MonoBehaviour
{
    public int depth = 20;
    public int width = 256;
    public int height = 256;
    public float heightScale = 20f;
    public float waterScale = 20f;
    public float waterThreshold = 0.1f;
    public float waterBuffer = 3f;
    public float waterPow = 1f;

    public float offsetX1 = 100f;
    public float offsetY1 = 100f;
    public float offsetX2 = 100f;
    public float offsetY2 = 100f;

    private void Start()
    {
        offsetX1 = Random.Range(0, 10000);
        offsetY1 = Random.Range(0, 10000);
        offsetX2 = Random.Range(0, 10000);
        offsetY2 = Random.Range(0, 10000);

    }

    private void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }
    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        
        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        
        float xCoord = (float)x / width * heightScale + offsetX1;
        float yCoord = (float)y / height * heightScale + offsetY1;

        float elevation = Mathf.PerlinNoise(xCoord, yCoord);
        return elevation + waterScale;
        /*
        if (elevation < 0.25f)
        {
            return elevation + 1;
        } else
        {
            return 3 * elevation + 3;
        }
        */
        /*
        float height = 5;

        height += 10 * ApplyHills(x, y);
        height -= ApplyLakes(x, y, height);

        return height;
        */
    }

    float ApplyHills(int x, int y)
    {
        float xCoord = (float)x / width * heightScale + offsetX1;
        float yCoord = (float)y / height * heightScale + offsetY1;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    float ApplyLakes(int x, int y, float height)
    {
        float xCoord = (float)x / width * waterScale + offsetX2;
        float yCoord = (float)y / height * waterScale + offsetY2;

        float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);
        if (noiseValue < 0.001f)
            noiseValue = 0.001f;
        if (height < waterThreshold)
            return waterBuffer / Mathf.Pow(noiseValue, waterPow);
        else return 0;
    }
}
