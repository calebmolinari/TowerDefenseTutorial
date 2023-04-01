using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MeshSettings : UpdatableData
{
    public float meshScale = 1f;

    public const int numSupportedLODs = 5;
    public const int numSupportedChunkSizes = 9;
    public static readonly int[] supportedChunkSizes = { 48, 72, 96, 120, 144, 168, 192, 216, 240 };

    [Range(0, numSupportedChunkSizes - 1)]
    public int chunkSizeIndex;

    // # of verts per line of a mesh at the highest level of detail
    // Includes 2 extra vertices which are excluded from mesh but are used for calculating normals
    public int numberOfVerticesPerLine
    {
        get
        {
            return supportedChunkSizes[chunkSizeIndex] + 5;
        }
    }

    public float meshWorldSize
    {
        get
        {
            return (numberOfVerticesPerLine - 3) * meshScale;
        }
    }
}
