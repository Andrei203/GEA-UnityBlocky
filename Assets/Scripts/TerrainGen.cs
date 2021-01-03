using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject cube;
    public float perlinfreq;
    public float perlinamp;
    public int chunkSize;
    // Start is called before the first frame update
    private void Start()
    {
        //generate world chunks with cubes 
        for (var x = 0; x <= chunkSize; x++)
            for(var y = 0; y <= chunkSize; y++)
                for(var z = 0; z <= chunkSize; z++)
                {
                    var perlin = Mathf.PerlinNoise(x / perlinfreq, z / perlinfreq) * perlinamp;
                    if(y < perlin)
                        Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
                }
    }

    // Update is called once per frame
}
