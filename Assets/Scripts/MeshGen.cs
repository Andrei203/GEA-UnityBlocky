using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(MeshFilter))]
public class MeshGen : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private int blockSize = 16;
    private int blockDepth = 16;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
       vertices = new Vector3[(blockSize + 1) * (blockDepth + 1)];

       for (int i = 0, z = 0; z <= blockDepth; z++)
       {
           for (int x = 0; x < blockSize; x++)
           {
               vertices[i] = new Vector3(x, 0 , z);
               i++;
           }
       }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);    
        }
    }
}
