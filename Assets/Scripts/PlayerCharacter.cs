using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New PlayerCharacter", menuName ="Kario Mart/Character")]

public class PlayerCharacter : ScriptableObject
{
    public GameObject characterPrefab;
    public string characterName;
    [Range(4, 10)] public int topSpeed = 4;
    [Range(4, 10)] public int acceleration = 4;
    [Range(4, 10)] public int handling = 4;
    [Range(4, 10)] public int grip = 4;
    
}
