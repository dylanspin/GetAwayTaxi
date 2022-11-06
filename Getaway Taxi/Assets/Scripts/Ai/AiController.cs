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
    [SerializeField] private StateManager stateScript;
    [SerializeField] private PatrolState patrolState;
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private NavMeshAgent agent;

    [Header("Set private data")]
    [SerializeField] private AiLook lookScript;
    [SerializeField] private Transform bodyHolder;

    [Header("private data")]
    private int layer = 0;
    private bool crashedBool = false;
    private GameObject spawnedBody;
    private AiCarInformation aiInformation;
    private AiManager managerScript;
    private CarBodyScript bodyScript = null;

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

    public void crashed()
    {
        bodyScript.setIcon(false);
        crashedBool = true;
    }

    public void setEnable(bool active)
    {
        if(!crashedBool)
        {
            if(active)
            {
                agent.Resume();
            }
            else
            {
                agent.Stop();
            }
        }
    }

    // public void setHeight(int newHeight)
    // {
    //     bodyScript.setIcon(layer == newHeight);
    // }
}
