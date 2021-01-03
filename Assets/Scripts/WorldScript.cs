using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScript : MonoBehaviour
{
    //instantiate chunk amount
    public GameObject chunkPrefab;

    public int chunkAmount;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0 ; x < chunkAmount; x++)
        for(int y = 0 ; y < chunkAmount; y++)
        for (int z = 0; z < chunkAmount; z++)
        {
            GameObject thisChunk = Instantiate(chunkPrefab,new Vector3(x * TerrainChunk.chunkWidth, y * TerrainChunk.chunkHeight,z * TerrainChunk.chunkDepth),Quaternion.identity);
            thisChunk.GetComponent<TerrainChunk>().GenerateTerrain(x, y ,z);
        }
    } 
}
