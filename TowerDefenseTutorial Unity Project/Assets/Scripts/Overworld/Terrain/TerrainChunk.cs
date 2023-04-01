using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TerrainChunk
{
    //Chunk position and mesh data
    public event System.Action<TerrainChunk, bool> onVisibilityChanged;
    public Vector2 coord;
    GameObject meshObject;
    Vector2 sampleCenter; //Not actually chunk center.
    Bounds bounds;
    Vector2 position;
    public Vector2 chunkCenter; //Actual chunk center
    Vector2 offsetToChunkCenter;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    LODInfo[] detailLevels;
    LODMesh[] lodMeshes;
    int colliderLODIndex;

    HeightMap heightMap;
    bool heightMapReceived;
    int previousLODIndex = -1;
    bool hasSetCollider = false;
    float maxViewDistance;
    const float colliderGenerationDistanceThreshold = 80f;

    HeightMapSettings heightMapSettings;
    MeshSettings meshSettings;
    Transform viewer;
    public GameObject markerCube;
    
    //Positioning of enemies and camps

    public TerrainChunk(Vector2 coord, HeightMapSettings heightMapSettings, MeshSettings meshSettings, LODInfo[] detailLevels, int colliderLODIndex, Transform parent, Transform viewer, Material material, GameObject markerCube)
    {
        this.coord = coord;
        this.detailLevels = detailLevels;
        this.colliderLODIndex = colliderLODIndex;
        this.heightMapSettings = heightMapSettings;
        this.meshSettings = meshSettings;
        this.viewer = viewer;
        this.markerCube = markerCube;
        sampleCenter = coord * meshSettings.meshWorldSize / meshSettings.meshScale;
        position = coord * meshSettings.meshWorldSize;
        bounds = new Bounds(position, Vector2.one * meshSettings.meshWorldSize);
        offsetToChunkCenter = new Vector2(-meshSettings.meshWorldSize / 2, meshSettings.meshWorldSize / 2);
        chunkCenter = (coord * meshSettings.meshWorldSize) + offsetToChunkCenter;

        meshObject = new GameObject("Terrain Chunk");
        meshRenderer = meshObject.AddComponent<MeshRenderer>();
        meshFilter = meshObject.AddComponent<MeshFilter>();
        meshCollider = meshObject.AddComponent<MeshCollider>();
        meshRenderer.material = material;

        meshObject.transform.position = new Vector3(position.x, 0, position.y);
        meshObject.transform.parent = parent;
        SetVisible(false);

        lodMeshes = new LODMesh[detailLevels.Length];
        for (int i = 0; i < detailLevels.Length; i++)
        {
            lodMeshes[i] = new LODMesh(detailLevels[i].lod);
            lodMeshes[i].updateCallback += UpdateTerrainChunk;
            if (i == colliderLODIndex)
            {
                lodMeshes[i].updateCallback += UpdateCollisionMesh;
            }
        }

        maxViewDistance = detailLevels[detailLevels.Length - 1].visibleDistanceThreshold;

        //UnityEngine.Object.Instantiate(markerCube, new Vector3(chunkCenter.x, 10, chunkCenter.y), Quaternion.identity);
    }

    public void Load()
    {
        ThreadedDataRequester.RequestData(() => HeightMapGenerator.GenerateHeightMap(meshSettings.numberOfVerticesPerLine, meshSettings.numberOfVerticesPerLine, heightMapSettings, sampleCenter), OnHeightMapReceived);
    }

    void OnHeightMapReceived(object heightMapObject)
    {
        this.heightMap = (HeightMap)heightMapObject;
        heightMapReceived = true;

        UpdateTerrainChunk();
    }

    Vector2 viewerPosition
    {
        get
        {
            return new Vector2(viewer.position.x, viewer.position.z);
        }
    }

    public void UpdateTerrainChunk()
    {
        if (heightMapReceived)
        {
            float viewerDistFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool wasVisible = IsVisible();
            bool visible = viewerDistFromNearestEdge <= maxViewDistance;

            if (visible)
            {
                int lodIndex = 0;

                for (int i = 0; i < detailLevels.Length - 1; i++)
                {
                    if (viewerDistFromNearestEdge > detailLevels[i].visibleDistanceThreshold)
                    {
                        lodIndex = i + 1;
                    }
                    else break;
                }
                if (lodIndex != previousLODIndex)
                {
                    LODMesh lodMesh = lodMeshes[lodIndex];
                    if (lodMesh.hasMesh)
                    {
                        previousLODIndex = lodIndex;
                        meshFilter.mesh = lodMesh.mesh;
                    }
                    else if (!lodMesh.hasRequestedMesh)
                    {
                        lodMesh.RequestMesh(heightMap, meshSettings);
                    }
                }
            }
            if (wasVisible != visible)
            {
                SetVisible(visible);
                if (onVisibilityChanged != null)
                {
                    onVisibilityChanged(this, visible);
                }
            }

        }
    }


    public void UpdateCollisionMesh()
    {
        if (!hasSetCollider)
        {
            float distanceFromViewerToChunkCenter = Vector3.Distance(viewerPosition, chunkCenter);

            if ((distanceFromViewerToChunkCenter * distanceFromViewerToChunkCenter) < detailLevels[colliderLODIndex].sqrVisibleDistanceThreshold)
            {
                if (!lodMeshes[colliderLODIndex].hasRequestedMesh)
                {
                    lodMeshes[colliderLODIndex].RequestMesh(heightMap, meshSettings);
                }
            }

            if (distanceFromViewerToChunkCenter < colliderGenerationDistanceThreshold)
            {
                if (lodMeshes[colliderLODIndex].hasMesh)
                {
                    meshCollider.sharedMesh = lodMeshes[colliderLODIndex].mesh;
                    hasSetCollider = true;
                }
            }
        }
    }

    public void SetVisible(bool visible)
    {
        meshObject.SetActive(visible);
    }

    public bool IsVisible()
    {
        return meshObject.activeSelf;
    }
}

class LODMesh
{
    public Mesh mesh;
    public bool hasRequestedMesh;
    public bool hasMesh;
    int lod;
    public event System.Action updateCallback;

    public LODMesh(int lod)
    {
        this.lod = lod;
    }

    void OnMeshDataReceived(object meshDataObject)
    {
        mesh = ((MeshData)meshDataObject).CreateMesh();
        hasMesh = true;

        updateCallback();
    }

    public void RequestMesh(HeightMap heightMap, MeshSettings meshSettings)
    {
        hasRequestedMesh = true;
        ThreadedDataRequester.RequestData(() => MeshGenerator.GenerateTerrainMesh(heightMap.values, meshSettings, lod), OnMeshDataReceived);
    }
}