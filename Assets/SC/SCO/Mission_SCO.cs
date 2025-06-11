using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "NewMission", menuName = "SCO/Mission")]
public class Mission_SCO : ScriptableObject
{
    public string scene_name;
    public string letter;

    [Header("camera")]
    public Vector2 minBounds;      
    public Vector2 maxBounds; 
}
