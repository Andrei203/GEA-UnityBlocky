using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Texture", menuName = "Texture")]

//calculate vector
public class blockTile : ScriptableObject
{
   public Vector2[] uvs = new Vector2[4];
}
