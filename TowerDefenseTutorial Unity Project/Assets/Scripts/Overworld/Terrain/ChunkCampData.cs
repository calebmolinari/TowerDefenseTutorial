using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkCampData
{
    public enum CampStatus { Empty, Adjacent, Occupied };
    public CampStatus campStatus;
    public bool campStatusIsPredetermined = false; // If another ChunkCampData has triggered the creation of this one, this bool will determine how the constructor functions
    public Vector2 coord;

    public ChunkCampData(Vector2 coord, bool campStatusIsPredetermined, Dictionary<Vector2, ChunkCampData> chunkCampDataDictionary, Dictionary<Vector2, TerrainChunk> terrainChunkDictionary)
    {
        this.coord = coord;
        this.campStatusIsPredetermined = campStatusIsPredetermined;

        if (campStatusIsPredetermined)
        {
            campStatus = CampStatus.Adjacent;
        } else
        {
            int random = Random.Range(1, 11);
            if (random >= TerrainGenerator.campThreshold)
            {
                // Set up this chunk as Occupied
                campStatus = CampStatus.Occupied;
                // Create debugging cube to show which chunks have camps
                UnityEngine.Object.Instantiate(terrainChunkDictionary[coord].markerCube, new Vector3(terrainChunkDictionary[coord].chunkCenter.x, 10, terrainChunkDictionary[coord].chunkCenter.y), Quaternion.identity);

                // Set up all 8 adjacent chunks as adjacent so there are never two adjacent occupied chunks
                for (int y = (int)coord.y + 1; y >= coord.y - 1; y--)
                {
                    for (int x = (int)coord.x - 1; x <= coord.x + 1; x++)
                    {
                        Vector2 currentCoord = new Vector2(x, y);
                        // The below code does not need to be executed on this ChunkCampData's coord, so the below check skips it
                        if (currentCoord == coord) 
                            break;
                        if (chunkCampDataDictionary.ContainsKey(currentCoord))
                        {
                            // Check if adjacent camp is occupied and give an error if so because this should never happen
                            if (chunkCampDataDictionary[currentCoord].campStatus == CampStatus.Occupied)
                            {
                                Debug.Log("The chunk at " + currentCoord.ToString() + " and " + coord.ToString() + " are both occupied and adjacent");
                            } else
                            {
                                chunkCampDataDictionary[currentCoord].campStatus = CampStatus.Adjacent;
                            }

                        } else
                        {
                            // ChunkCampData does not exist at currentCoord so we need to
                            // Create new ChunkCampData and add it to dictionary
                            ChunkCampData newChunkCampData = new ChunkCampData(currentCoord, true, chunkCampDataDictionary, terrainChunkDictionary);
                            chunkCampDataDictionary.Add(currentCoord, newChunkCampData);
                        }
                    }
                }
            }
        }
    }
}
