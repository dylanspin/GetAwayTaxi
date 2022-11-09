using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AiCar", menuName = "Scriptables/AiCar")]

public class AiCarInformation : ScriptableObject
{
    [Header("Strings")]
    public string Name = "AI-Car";//the name of this scriptable object

    [Header("Gameobjects")]
    public GameObject spawnObject;//the visual body of the AI

    [Header("Patrolling")]

    [Tooltip("The min and max speed the car gets for patrolling")]
    public Vector2 patrolSpeed = new Vector2(3,9);//the speed the AI wanders/patrols trough the city

    [Tooltip("The min and max speed the car gets for chasing")]
    public Vector2 chaseSpeed = new Vector2(10,15);//the speed for the police when chasing the player

    [Header("Ints")]
    public int rarity = 1;//the amount of times this scriptable objects gets added to a list for the spawning rarity

    [Header("Bools")]
    public bool police = false;//if this ai is police so if it can chase the player
}
