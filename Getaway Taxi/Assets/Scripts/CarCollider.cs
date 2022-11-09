using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour
{

    [Header("Crash settings")]
    
    [Tooltip("Added collision force multiplier")]
    [SerializeField] private Vector2 forceMultiplier = new Vector2(10,20);//min and max value used for the random force multiplier on collided cars

    [Tooltip("BackForce on player car")]
    [SerializeField] private Vector2 backwardsForce = new Vector2(10,20);//min and max value used for the random force multiplier on the player car

    [Tooltip("The time the cops are disabled")]
    [SerializeField] private float copDisableTime = 5f;//the time cop cars are disabled when collided with cop
    
    [Header("Components")]
    
    [Tooltip("RigidBody")]
    [SerializeField] private Rigidbody carRb;//rigidbody of the car

    [Tooltip("Lights to indicate health")]
    [SerializeField] Animator[] lightAnimator;//the health lights

    [Header("Private scripts")]
    private CarStats statsScript;//center car statestics script
    private Car carMovementScript;//the movement script of the car
    private CarAudio audioScript;//the script that controlls the audio of the car
    private CarController controller;//the main controller of the car

    [Header("Private Data")]
    private int health = 3;//curent health of the car
    private bool collidDelay = true;//if the car can lose life from cop cars

    public void setStartData(Car newMovement,CarStats newStats,CarController controllerScript,CarAudio newAudio)//sets the start scripts from the car controller script
    {
        carMovementScript = newMovement;
        statsScript = newStats;
        audioScript = newAudio;
        controller = controllerScript;
        health = lightAnimator.Length;//sets the start max health to the amount of light objects 
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!Values.pauzed)
        { 
            GameObject rootObject = other.transform.root.gameObject;
            if(rootObject.layer == LayerMask.NameToLayer("Ai"))
            {
                bool collided = rootObject.GetComponent<StateManager>().setCrashed(transform.forward * statsScript.getSpeed() * Random.Range(forceMultiplier.x,forceMultiplier.y));
                
                if(!collided)//if the AI car is still alive
                {
                    carRb.AddForce(transform.forward * -statsScript.getSpeed() * Random.Range(backwardsForce.x,backwardsForce.y));
                }
            }

            if(collidDelay && rootObject.tag == "Police")//if the delat is not active and that the collision was with a police car
            {
                loseHealth();
            }
            
            audioScript.collide(other.contacts[0].point);
            carMovementScript.collision(statsScript.getSpeed());
        }
    }

    private void reactivateCollider()//allows the player to lose a life again after collision with cop car
    {
        collidDelay = true;
    }

    private void loseHealth()//does the health check when collided with police car
    {
        if(health > 1)
        {
            health --;
            controller.disableCops(copDisableTime);//disables cops for a couple of seconds
        }
        else
        {
            health = 0;
            controller.lost();//loses the game via the controller script
        }
        collidDelay = false;//turns of the can collide lose life bool
        lightAnimator[health].SetBool("health",false);//turns of the life light
        Invoke("reactivateCollider",copDisableTime);//allows the player to lose a life again after the disable time
    }
}
