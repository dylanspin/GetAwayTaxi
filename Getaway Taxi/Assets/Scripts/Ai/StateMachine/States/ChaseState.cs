using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    [Header("AI Components")]

    [Tooltip("Nav Mesh Agent of the Ai")]
    [SerializeField] private NavMeshAgent agent;//the nav mesh agent component for pathfinding 

    [Header("Private Data")]
    private CarBodyScript bodyScript;//the visual body script for turning on and off the police lights
    private AiCarInformation carInfo;//the scriptable object of the current AI
    private AiManager managerScript;//the manager script of all the AI
    private Transform target;//the following target so the player

    [Header("State machine values")]
    public PatrolState patrolState;//the wandering/patrol state
    public bool canSeePlayer;//if the player is in view

    public void setStart(AiManager newManager, AiCarInformation info,CarBodyScript newBodyScript)//start function gets called from the controller of the car
    {
        managerScript = newManager;
        carInfo = info;
        bodyScript = newBodyScript;
    }

    public override State runThisState()//the update function for this state
    {
        if(canSeePlayer)//if player is in view run chase function
        {
            chase();
            return this;//keeps the state to the current one
        }
        else//if player not inview stop the chase state via the stopchase function
        {
            stopChase();
            return patrolState;//switches the state to the patroling state
        }
    }

    private void chase()
    {
        agent.SetDestination(target.position);//sets the destination of the navmesh agent to the target position
    }

    private void stopChase()
    {
        patrolState.setDest(managerScript.getClosedNext(transform.root.transform));//sets the destination to the closed point
    }

    public void setStartState(Transform newTarget)//when the state is just started
    {
        canSeePlayer = true;
        agent.speed = Random.Range(carInfo.chaseSpeed.x,carInfo.chaseSpeed.y);//sets random chase speed based on the min and max chase value on the scriptable object
        target = newTarget;//sets the target to the player object
      
        bodyScript.setChase(true);//turns on chase lights
    }

    public void outOfView()//when the player is out of view
    {
        canSeePlayer = false;
        bodyScript.setChase(false);//turns off chase lights
    }
    
}
