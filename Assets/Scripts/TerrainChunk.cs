using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class TerrainChunk : MonoBehaviour
{
    // Start is called before the first frame update
    // chunk standard size
    public const int chunkHeight = 32;
    public const int chunkWidth = 32;
    public const int chunkDepth =  48;
    public block fillerBlock;
    public block midBlock;
    public block surfaceBlock;
    public float perlinfreq;
    public float perlinamp;
    public block[,,] blocks = new block[chunkWidth,chunkHeight,chunkDepth];
    void Start()
    
    { /*
        for(int x = 0; x < chunkWidth; x++)
        for (int y = 0; y < chunkHeight; y++)
        for(int z = 0; z < chunkDepth; z++)
        {
            
            var perlin = Mathf.PerlinNoise(x / perlinfreq, z / perlinfreq) * perlinamp;
            if (y < perlin)
            {
                blocks[x, y, z] = fillerBlock;
                if (y + 4 >= perlin)
                { 
                    blocks[x, y, z] = midBlock;
                }
                                              
                if (y + 1 >= perlin)
                {
                 blocks[x, y, z] = surfaceBlock;
                } 
            } 
        }
    
    GenerateTerrain(0,0,0);
     
        GenerateMesh();
        */
    }

    public void GenerateTerrain(int chunkX, int chunkY, int chunkZ)
    {
        for(int x = 0; x < chunkWidth; x++)
        for (int y = 0; y < chunkHeight; y++)
        for(int z = 0; z < chunkDepth; z++)
        {
            
            var perlin = Mathf.PerlinNoise((x + chunkX * chunkWidth) / perlinfreq, (z + chunkZ * chunkDepth) / perlinfreq) * perlinamp;
            if (y + chunkY * chunkHeight  < perlin)
            {
                blocks[x, y, z] = fillerBlock;
                if (y + chunkY * chunkHeight + 4 >= perlin)
                { 
                    blocks[x, y, z] = midBlock;
                }
                                              
                if ((y + chunkY * chunkHeight) + 1 >= perlin)
                {
                    blocks[x, y, z] = surfaceBlock;
                }
            }
        }
        GenerateMesh();
    }
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int x = 0; x < chunkWidth; x++)
        {
            for (int z = 0; z < chunkDepth; z++)
            {
                for (int y = 0; y < chunkHeight; y++)
                {
                    if (blocks[x, y, z] == null) continue;
                    
                    int faceCounter = 0;
                    Vector3 thisPos = new Vector3(x, y, z);
                    // Top
                    if (y == chunkHeight - 1 || blocks[x, y + 1, z] == null)
                    {
                        Debug.Log("d");
                        verts.Add(thisPos + new Vector3(0, 1, 0));
                        verts.Add(thisPos + new Vector3(0, 1, 1));
                        verts.Add(thisPos + new Vector3(1, 1, 1));
                        verts.Add(thisPos + new Vector3(1, 1, 0));
                        faceCounter++;
                        uvs.AddRange(blocks[x, y, z].textures[0].uvs);
                    }

                    // Bottom
                    if (y == 0 || blocks[x, y - 1, z] == null)
                    {
                        verts.Add(thisPos + new Vector3(0, 0, 0));
                        verts.Add(thisPos + new Vector3(1, 0, 0));
                        verts.Add(thisPos + new Vector3(1, 0, 1));
                        verts.Add(thisPos + new Vector3(0, 0, 1));
                        faceCounter++;
                        uvs.AddRange(blocks[x, y, z].textures[1].uvs);
                    }

                    // Front
                    if (z == 0 || blocks[x, y, z - 1] == null)
                    {
                        verts.Add(thisPos + new Vector3(0, 0, 0));
                        verts.Add(thisPos + new Vector3(0, 1, 0));
                        verts.Add(thisPos + new Vector3(1, 1, 0));
                        verts.Add(thisPos + new Vector3(1, 0, 0));
                        faceCounter++;
                        uvs.AddRange(blocks[x, y, z].textures[2].uvs);
                    }

                    // Back
                    if (z == chunkDepth - 1 || blocks[x, y, z + 1] == null)
                    {
                        verts.Add(thisPos + new Vector3(1, 0, 1));
                        verts.Add(thisPos + new Vector3(1, 1, 1));
                        verts.Add(thisPos + new Vector3(0, 1, 1));
                        verts.Add(thisPos + new Vector3(0, 0, 1));
                        faceCounter++;
                        uvs.AddRange(blocks[x, y, z].textures[4].uvs);
                    }

                    // Left
                    if (x == 0 || blocks[x - 1, y, z] == null)
                    {
                        verts.Add(thisPos + new Vector3(0, 0, 1));
                        verts.Add(thisPos + new Vector3(0, 1, 1));
                        verts.Add(thisPos + new Vector3(0, 1, 0));
                        verts.Add(thisPos + new Vector3(0, 0, 0));
                        faceCounter++;
                        uvs.AddRange(blocks[x, y, z].textures[3].uvs);
                    }

                    // Right
                    if (x == chunkWidth - 1 || blocks[x + 1, y, z] == null)
                    {
                        verts.Add(thisPos + new Vector3(1, 0, 0));
                        verts.Add(thisPos + new Vector3(1, 1, 0));
                        verts.Add(thisPos + new Vector3(1, 1, 1));
                        verts.Add(thisPos + new Vector3(1, 0, 1));
                        faceCounter++;
                        uvs.AddRange(blocks[x, y, z].textures[5].uvs);
                    }

                    // Triangles
                    int vertCountOffset =
                        verts.Count - 4 * faceCounter; // Gets this block's vertices' offset from the start of the list
                    for (int i = 0; i < faceCounter; i++)
                    {
                        tris.Add(vertCountOffset + i * 4);
                        tris.Add(vertCountOffset + i * 4 + 1);
                        tris.Add(vertCountOffset + i * 4 + 2); // tri 1
                        tris.Add(vertCountOffset + i * 4);
                        tris.Add(vertCountOffset + i * 4 + 2);
                        tris.Add(vertCountOffset + i * 4 + 3); // tri 2
                    }
                }
            }
        }
        
        mesh.vertices = verts.ToArray();

        mesh.triangles = tris.ToArray();

        mesh.uv = uvs.ToArray();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
