using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    /*
        Controller script of individual ai
    */
    
    [Header("States data")]
    [SerializeField] private StateManager stateScript;//the statemachine manager
    [SerializeField] private PatrolState patrolState;//the patroling state
    [SerializeField] private ChaseState chaseState;//the chasing the player state
    [SerializeField] private NavMeshAgent agent;//the navmesh agent component for pathfinding on the navmesh

    [Header("Set private data")]
    [SerializeField] private AiLook lookScript;//the AI look script that checks if the player is in view
    [SerializeField] private Transform bodyHolder;//the holder transform that holds the visual object of the AI from the scriptable object

    [Header("private data")]
    private int layer = 0;//the current layer spawned on
    private bool crashedBool = false;//if crashed
    private GameObject spawnedBody;//the spawned visual body of the AI 
    private AiCarInformation aiInformation;//the scriptable object holding the information for the AI
    private AiManager managerScript;//the manager script of all the AI in the scene
    private CarBodyScript bodyScript = null;//the script of on the visual body of the AI 

    public void setStartInformation(AiCarInformation newInformation,AiManager newManager,Transform startPos,int spawnLayer)
    {   
        layer = spawnLayer;
        aiInformation = newInformation;
        managerScript = newManager;
        patrolState.setStart(newInformation,startPos);
        stateScript.setStart(this);
        spawncarBody();
        lookScript.enabled = newInformation.police;//disable for testing
    }

    public void spawncarBody()//spawns the body of the car
    {
        spawnedBody = Instantiate(aiInformation.spawnObject,bodyHolder.position,bodyHolder.rotation,bodyHolder);
        bodyScript = spawnedBody.GetComponent<CarBodyScript>();
        bodyScript.setIcon(layer == 1);
        chaseState.setStart(managerScript,aiInformation,bodyScript);
    }

    public void crashed()//sets the AI to crashed
    {
        bodyScript.setIcon(false);//turns of the icon for the mini-map when crashed
        crashedBool = true;
    }

    public void setEnable(bool active)//turns on or of the AI pathfinding 
    {
        if(!crashedBool)//if the AI is not crashed
        {
            if(active)
            {
                agent.Resume();//continues the pathfindinf of the AI
            }
            else
            {
                agent.Stop();//stops the pathfinding of the AI
            }
        }
    }

    // public void setHeight(int newHeight)//was used when the player could still change heights between different layers
    // {
    //     bodyScript.setIcon(layer == newHeight);
    // }
}
