using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{   
    [Header("Nav Information")]

    [Tooltip("Route positions")]
    [SerializeField] private List<Transform> routePoints = new List<Transform>();//all route points on the map for finding the closest one

    [Header("Cops Spawn Settings")]
    
    [Tooltip("Time between new car spawns")]
    [SerializeField] private float copSpawnTime = 0.2f;//time between cop spawns

    [Tooltip("Max amount cops spawned cars in the world")]
    [SerializeField] private int maxCops = 50;//max amount of cops spawned on the map

    [Tooltip("Cop Spawn positions")]
    [SerializeField] private List<Transform> copSpawns = new List<Transform>();//cop spawn positions

    [Tooltip("Spawn car informations")]
    [SerializeField] private AiCarInformation[] copAis;//cop ai scriptable object if we wanted different types of cops
    
    [Header("Civ Spawn Settings")]

    [Tooltip("Time between new car spawns")]
    [SerializeField] private float timeBetweenSpawns = 0.2f;//time between civ car spawns

    [Tooltip("Max amount of default spawned cars on each spawn height")]
    [SerializeField] private int[] maxCiv = {100,50,100,100};//max amount of civ cars on different heights

    [Tooltip("Spawn positions")]
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();//civ car spawn points

    [Tooltip("Spawn car informations")]
    [SerializeField] private AiCarInformation[] civAi;//civ car scriptable objects that hold the data for different AI vehicles

    [Tooltip("Base movement car")]
    [SerializeField] private GameObject spawnCarObj;//the base movement object for all the AI 


    [Header("Private Information")]
    private List<Transform> spawnedCars = new List<Transform>();//all spawned civ vehicles
    private List<Transform> spawnedCops = new List<Transform>();//all spawned cop vehicles
    private List<int> aiRarities = new List<int>();//a list that contains the scriptable objects multiple times the amount of the rarity 
    private List<int> copRarities = new List<int>();//a list that contains the scriptable objects multiple times the amount of the rarity 

    private void Start()
    {
        setAiRarities();//adds the the scriptable object multiple times the amount of the rarity of the object
        startSpawn();//stars spawning vehicles
    }

    /////////////spawning "Ai" ///has duplicate code for now can be better optimized 

    private void setAiRarities()//makes a list with all ids of the ai's for a simple rarity effect
    {
        for(int i=0; i<civAi.Length; i++)
        {
            for(int b=0; b<civAi[i].rarity; b++)
            {
                aiRarities.Add(i);
            }
        }

        for(int i=0; i<copAis.Length; i++)
        {
            for(int b=0; b<copAis[i].rarity; b++)
            {
                copRarities.Add(i);
            }
        }
    }

    private void startSpawn()//starts spawning vehicles
    {
        for(int i=0; i<spawnPoints.Count; i++)
        {
            spawnCar(i);//spawns cop car 
        }

        for(int i=0; i<copSpawns.Count; i++)
        {
            spawnCop(i);//spawns cop car 
        }

        Invoke("randomCopSpawn",copSpawnTime);
        Invoke("spawnRandomSpot",timeBetweenSpawns);
    }

    //invokes
    private void spawnRandomSpot()
    {
        int spawnPoint = Random.Range(0,spawnPoints.Count-1);
        Invoke("spawnRandomSpot",timeBetweenSpawns);
        spawnCar(spawnPoint);
    }

    private void randomCopSpawn()
    {
        int spawnPoint = Random.Range(0,copSpawns.Count-1);
        Invoke("randomCopSpawn",copSpawnTime);
        spawnCop(spawnPoint);
    }

    private void spawnCar(int spawnPoint)
    {
        int b = maxCiv.Length;
        for(int i=0; i<maxCiv.Length; i++)//spawn on all layers
        {
            if(maxCiv[i] > 0)//if it can still spawn AI on this layer
            {
                maxCiv[i] --;//removes from the max count on the current layer
                b--;

                Transform spawnPos = spawnPoints[spawnPoint].GetChild(i);//the spawn points have 4 different child objects on the 4 height layers
                
                AiCarInformation currentAi = civAi[aiRarities[Random.Range(0,aiRarities.Count)]];//the current AI scriptable object

                Transform spawnedAi = Instantiate(spawnCarObj,spawnPos.position,spawnPos.rotation).transform;//spawn AI base object
                Transform startDes = spawnPoints[spawnPoint].GetComponent<NextPoint>().nextPoint();//sets the first start destination

                AiController controllerScript = spawnedAi.GetComponent<AiController>();//the controller of the invidual AI
                controllerScript.setStartInformation(currentAi,this,startDes,i);//sets the start information on the AI 

                spawnedCars.Add(spawnedAi);//adds spawned AI to list
            }
        }

        if(b == maxCiv.Length)//check if all spawned
        {
            CancelInvoke("spawnRandomSpot");
        }
    }

    private void spawnCop(int spawnPoint)
    {
        if(maxCops > 0)
        {
            maxCops --;//removes from the max count

            Transform spawnPos = copSpawns[spawnPoint];//the position of the cop spawn
                
            AiCarInformation currentAi = copAis[copRarities[Random.Range(0,copRarities.Count)]];//the scriptable object of the AI
            
            Transform spawnedAi = Instantiate(spawnCarObj,spawnPos.position,spawnPos.rotation).transform;//spawns base object of the AI
            spawnedAi.tag = "Police";//set the tag of the object for checking collisions
            Transform startDes = copSpawns[spawnPoint].GetComponent<NextPoint>().nextPoint();//sets the first destination of the AI

            AiController controllerScript = spawnedAi.GetComponent<AiController>();//the controller of the invidual AI
            controllerScript.setStartInformation(currentAi,this,startDes,1);//sets the ai controller start information

            spawnedCops.Add(spawnedAi);//adds AI to list 
        }
    }

    public Transform getClosedNext(Transform carTrans)//after done chasing the player the police ai gets the closed point 
    {
        Transform closedPos = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = carTrans.position;
        foreach (Transform pos in routePoints)
        {
            float dist = Vector3.Distance(pos.position, currentPos);
            if (dist < minDist)
            {
                closedPos = pos;
                minDist = dist;
            }
        }
        
        return closedPos;
    }

    /* 
        the old point system where they get a random point assigned but cant be the same as the old point
        Not used anymore
    */
    public Transform getNewPoint(Transform lastPos)
    {
        Transform newReturn = null;
        if(lastPos == null)
        {
            newReturn = routePoints[Random.Range(0,routePoints.Count-1)];
        }
        else
        {
            while(newReturn == null || newReturn == lastPos)
            {
                newReturn = routePoints[Random.Range(0,routePoints.Count-1)];
            }
        }

        return newReturn;
    }

    public void disableCops(float disableTime)
    {
        CancelInvoke("enableCops");//makes sure there are no double invokes
        Invoke("enableCops",disableTime);//calls the enable all cop cars time after the delay

        setAllCops(false);//disables all cop cars
    }

    private void enableCops()//enable all cop cars after the disable time again
    {
        setAllCops(true);
    }

    private void setAllCops(bool active)//enables or disables all spawned cop cars
    {
        for(int i=0; i<spawnedCops.Count; i++)
        {
            if(spawnedCops[i] != null)//if the object excist
            {
                spawnedCops[i].GetComponent<AiController>().setEnable(active);//enables or disables the spawned cop car
            }
        }
    }
}
