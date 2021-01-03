using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//textures
[CreateAssetMenu(fileName = "New Block", menuName = "Block")]
public class block : ScriptableObject
{
    public blockTile[] textures = new blockTile[6];
}
