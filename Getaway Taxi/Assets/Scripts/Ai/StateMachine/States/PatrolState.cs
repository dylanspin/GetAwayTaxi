using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{

    [Header("AI Settings")]

    [Tooltip("Main AI Manager Script")]
    [SerializeField] private float stoppingDistance = 10;//the distance before the car stops from the end destination

    [Header("AI Components")]

    [Tooltip("Nav Mesh Agent of the Ai")]
    [SerializeField] private NavMeshAgent agent;//the navmesh agent component for pathfinding on the navmesh

    [Header("Private Data")]
    private AiCarInformation carInfo;//the scriptable object of the current AI for the related information
    private Transform currentPos = null;//the current end destination
    private Vector2 goV2;//the end destination position on 2d space instead of 3d
    private float remainDistance;//so i can see it in the debugger
    private float patrolSpeed = 0;//the speed of the ai movement

    [Header("Statemachine public data")]
    public bool canSeePlayer;//if the ai can see the player is set from the aiLook script
    public ChaseState ChaseState;//the next state if the player is in view 

    public void setStart(AiCarInformation info,Transform startDest)//start function gets called from the controller of the car
    {
        carInfo = info;//sets the scriptable object
        setStats();//sets the random stats based on the scriptable object of the Ai
        setDest(startDest);//sets the first destination
    }

    private void setStats()//if there where more stats needed to be set with future functions
    {
        setRandomSpeed();//sets random speed for the patroling
    }

    private void setRandomSpeed()//sets random speed for the patroling
    {
        patrolSpeed = Random.Range(carInfo.patrolSpeed.x,carInfo.patrolSpeed.y);
        agent.speed = patrolSpeed;
    }

    public override State runThisState()//update function of the state machine
    {
        if(canSeePlayer)
        {
            return ChaseState;//if the player is in view return the chase state so it switches to that state
        }
        else
        {
            movement();//movement of the AI function

            return this;//returns this state so it stays in this state
        }
    }

    private void movement()//the movement of the AI function
    {
        if(currentPos != null)//if it has end position continue
        {
            if(getDistance() <= stoppingDistance)//if close enough to the end destination
            {
                setNextPoint();//set new end destination
            }
        }
    }

    private float getDistance()//gets the current distance between the Ai and the set end point
    {
        Vector2 currentV2 = new Vector2(transform.position.x,transform.position.z);
        remainDistance = (goV2-currentV2).magnitude;
        
        return remainDistance;
    }   

    public void setNextPoint()//sets new end point for the AI
    {
        Transform newPostition = currentPos.GetComponent<NextPoint>().nextPoint();//gets new point from the old point so it gets the next posible points the ai can go to 
        setDest(newPostition);//sets the new destination of the end point
    }

    public void setDest(Transform newPostition)//sets the new destination of the end point
    {
        currentPos = newPostition;//sets the new end point to the given one
        goV2 = new Vector2(currentPos.position.x,currentPos.position.z);//makes the 2d position from the 3d position of the end point
        Vector3 newDest = new Vector3(currentPos.position.x,transform.root.transform.position.y,currentPos.position.z);//sets the new destination of the end point with the current height of the car
        agent.SetDestination(newDest);//sets the destination for the navmesh agent 
        setRandomSpeed();//sets new random patroling speed
    }
}
