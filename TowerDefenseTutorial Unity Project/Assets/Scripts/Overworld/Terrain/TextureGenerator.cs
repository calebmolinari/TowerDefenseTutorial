using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator
{
    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(HeightMap heightMap)
    {
        int width = heightMap.values.GetLength(0);
        int height = heightMap.values.GetLength(1);

        Color[] colorMap = new Color[width * height];
        int i = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = width - 1; x >= 0; x--)
            {
                colorMap[i] = Color.Lerp(Color.black, Color.white, Mathf.InverseLerp(heightMap.minValue, heightMap.maxValue, heightMap.values[x, y]));
                i++;
            }
        }
        return TextureFromColorMap(colorMap, width, height);
    }

    public static Texture2D CreateTextureForCampDistribution()
    {
        int width = 30;
        int height = 30;
        int exclusionZoneRadius = 2;

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int random = Random.Range(1, 11);
                if (random >= 9 && ((x < -exclusionZoneRadius || x > exclusionZoneRadius) || (y < -exclusionZoneRadius || y > exclusionZoneRadius)))
                    colorMap[y * width + x] = Color.black;
                else colorMap[y * width + x] = Color.white;
            }
        }
        return TextureFromColorMap(colorMap, width, height);
    }
}
