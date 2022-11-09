using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiLook : MonoBehaviour
{
    [Header("States")]
    
    [Tooltip("Distance checked for the player car")]
    [SerializeField] private ChaseState chaseState;//the state for the police for chasing the player
    [SerializeField] private AmbushState ambushState;//if we wanted to add a ambush state
    [SerializeField] private PatrolState patrolState;//random wandering trough the city via the points state
    
    [Header("check Settings")]

    [Tooltip("Distance checked for the player car")]
    [SerializeField] private float viewDistance = 300;//the distance the raycast are to check if the player is still in view
    
    [Tooltip("Distance checked for the player car")]
    [SerializeField] private float triggerDistance = 40;//the trigger distance for checking if the car should check if the player is in view

    [Tooltip("Time Player can not be inview ")]
    [SerializeField] private float maxNoViewTime = 50;//the amount of times the player can not be in view for the AI to stop seeing it

    [Tooltip("Distance checked for police cars")]
    [SerializeField] private LayerMask checkLayers;//the trigger sphere layers 
    [SerializeField] private LayerMask viewLayers;//the layers that should block the raycast

    [Header("Private data")]
    private bool inTrigger = false;//if the player is in the trigger
    private bool inView = false;//if the player is in the view
    private float viewTime = 0;//how many times the player is in view 
    private Transform player;//the player object

    void FixedUpdate()
    {
        if(!inTrigger)
        {
            checkCopRadius();//check if in trigger sphere 
        }
        else
        {
            checkView();//check with raycast if the player is in view
        }
    }

    private void checkCopRadius()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, triggerDistance,checkLayers);//cast sphere cast
        if(hitColliders.Length > 0)//if it collided with something that means the player is in the cast
        {
            setIntrigger(hitColliders[0].transform);//sets if the player is in trigger
        }
    }

    private void setIntrigger(Transform hitTransform)//sets the start in trigger 
    {
        player = hitTransform.root.transform;
        inTrigger = true;
        viewTime = 0;//resets the amount in view
    }

    private void checkView()//check with 
    {
        if(player)//if has player object to prevent bug
        {
            if(Vector3.Distance(transform.position, player.position) < viewDistance)//check if the player is to far if so dont raycast
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, (player.position - transform.position));//raycast in the direction of the player
                if(Physics.Raycast(ray, out hit, viewDistance, viewLayers))
                {
                    setViewTime(hit.transform.root.gameObject.tag == "Player");//if player is hit return true
                }
                else
                {
                    setViewTime(false);//not in view
                }
            }
        }
    }

    private void setViewTime(bool see)//add or removed from the amount in view
    {
        if(see)
        {
            if(!inView)
            {   
                setInview(true);//in view
            }
            if(viewTime < maxNoViewTime)
            {
                viewTime ++;
            }
        }
        else
        {
            if(viewTime > 0)
            {
                viewTime --;
            }
            else
            {
                if(inView)
                {
                    setInview(false);//out of view
                }
            }
        }
    }

    private void setInview(bool active)//set the inview bool for the states
    {
        inView = active;
        if(active)//in view
        {
            viewTime = maxNoViewTime;
            chaseState.setStartState(player);
        }
        else//ouyt of view
        {
            chaseState.outOfView();//sets the player out of view
        }
        setStates();//sets all states inview bool
    }

    /*Draws sphere around the player for visualizing the range of the trigger*/
    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawSphere(transform.position, triggerDistance);
    // }

    private void setStates()//sets the state values for the state machine
    {
        chaseState.canSeePlayer = inView;
        ambushState.canSeePlayer = inView;
        patrolState.canSeePlayer = inView;
    }
}
