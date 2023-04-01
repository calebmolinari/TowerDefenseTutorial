using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int colliderLODIndex;

    const float viewerMoveThresholdForChunkUpdate = 25f;
    const float sqrViewerMoveThresholdForChunkUpdate = viewerMoveThresholdForChunkUpdate * viewerMoveThresholdForChunkUpdate;
    public LODInfo[] detailLevels;
    public Transform viewer;
    public Material mapMaterial;
    public GameObject markerCube;

    public MeshSettings meshSettings;
    public HeightMapSettings heightMapSettings;
    public TextureData textureSettings;

    Vector2 viewerPosition;
    Vector2 viewerPositionOld;
    static MapGenerator mapGenerator;
    float meshWorldSize;
    int chunksInViewDistance;

    public static int campThreshold = 10; // Used by ChunkCampData to determine camp status

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> visibleTerrainChunks = new List<TerrainChunk>();
    Dictionary<Vector2, ChunkCampData> chunkCampDataDictionary = new Dictionary<Vector2, ChunkCampData>();

    private void Start()
    {
        textureSettings.ApplyToMaterial(mapMaterial);
        textureSettings.UpdateMeshHeights(mapMaterial, heightMapSettings.minHeight, heightMapSettings.maxHeight);
        mapGenerator = FindObjectOfType<MapGenerator>();
        float maxViewDist = detailLevels[detailLevels.Length - 1].visibleDistanceThreshold;
        meshWorldSize = meshSettings.meshWorldSize;
        chunksInViewDistance = Mathf.RoundToInt(maxViewDist / meshWorldSize);
        UpdateVisibleChunks();
    }

    private void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);

        if (viewerPosition != viewerPositionOld)
        {
            foreach (TerrainChunk chunk in visibleTerrainChunks)
            {
                chunk.UpdateCollisionMesh();
            }
        }

        if ((viewerPositionOld - viewerPosition).sqrMagnitude > sqrViewerMoveThresholdForChunkUpdate)
        {
            viewerPositionOld = viewerPosition;
            UpdateVisibleChunks();
        }
        
    }

    void UpdateVisibleChunks()
    {
        HashSet<Vector2> alreadyUpdatedChunkCoords = new HashSet<Vector2>();
        for (int i = visibleTerrainChunks.Count - 1; i >= 0; i--)
        {
            alreadyUpdatedChunkCoords.Add(visibleTerrainChunks[i].coord);
            visibleTerrainChunks[i].UpdateTerrainChunk();
        }

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / meshWorldSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / meshWorldSize);

        for (int yOffset = -chunksInViewDistance; yOffset <= chunksInViewDistance; yOffset++)
        {
            for (int xOffset = -chunksInViewDistance; xOffset <= chunksInViewDistance; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
                if (!alreadyUpdatedChunkCoords.Contains(viewedChunkCoord))
                {
                    if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
                    {
                        terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
                    }
                    else
                    {
                        CreateNewChunk(viewedChunkCoord);
                    }
                }
            }
        }
    }

    void CreateNewChunk(Vector2 viewedChunkCoord)
    {
        TerrainChunk newChunk = new TerrainChunk(viewedChunkCoord, heightMapSettings, meshSettings, detailLevels, colliderLODIndex, transform, viewer, mapMaterial, markerCube);
        terrainChunkDictionary.Add(viewedChunkCoord, newChunk);
        newChunk.onVisibilityChanged += OnTerrainChunkVisibilityChanged;
        newChunk.Load();

        // if chunkcampdata at this coord does not exist, create it
        // this is one of two ways chunkcampdata can be created. this is independent creation, meaning its camp status was not set by an adjacent chunk
        if (!chunkCampDataDictionary.ContainsKey(viewedChunkCoord))
        {
            ChunkCampData newChunkCampData = new ChunkCampData(viewedChunkCoord, false, chunkCampDataDictionary, terrainChunkDictionary);
            chunkCampDataDictionary.Add(viewedChunkCoord, newChunkCampData);
        }
    }

    void OnTerrainChunkVisibilityChanged(TerrainChunk terrainChunk, bool isVisible)
    {
        if (isVisible)
        {
            visibleTerrainChunks.Add(terrainChunk);
        } else
        {
            visibleTerrainChunks.Remove(terrainChunk);
        }
    }


}

[System.Serializable]
public struct LODInfo
{
    [Range(0, MeshSettings.numSupportedLODs - 1)]
    public int lod;
    public float visibleDistanceThreshold;

    public float sqrVisibleDistanceThreshold
    {
        get { return visibleDistanceThreshold * visibleDistanceThreshold; }
    }
}