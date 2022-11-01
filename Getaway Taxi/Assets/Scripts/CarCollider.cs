using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour
{

    [Header("Crash settings")]
    
    [Tooltip("Added collision force multiplier")]
    [SerializeField] private Vector2 forceMultiplier = new Vector2(10,20);

    [Tooltip("BackForce on player car")]
    [SerializeField] private Vector2 backwardsForce = new Vector2(10,20);

    [Tooltip("The time the cops are disabled")]
    [SerializeField] private float copDisableTime = 5f;
    
    [Header("Components")]
    
    [Tooltip("RigidBody")]
    [SerializeField] private Rigidbody carRb;

    [Tooltip("Lights to indicate health")]
    [SerializeField] Animator[] lightAnimator;

    [Header("Private scripts")]
    private CarStats statsScript;
    private Car carMovementScript;
    private CarAudio audioScript;
    private CarController controller;

    [Header("Private Data")]
    private int health = 3;
    private bool collidDelay = true;

    public void setStartData(Car newMovement,CarStats newStats,CarController controllerScript,CarAudio newAudio)
    {
        carMovementScript = newMovement;
        statsScript = newStats;
        audioScript = newAudio;
        controller = controllerScript;
        health = lightAnimator.Length;
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

            if(collidDelay)
            {
                loseHealth();
            }
            
            audioScript.collide(other.contacts[0].point);
            carMovementScript.collision(statsScript.getSpeed());
        }
    }

    private void reactivateCollider()
    {
        collidDelay = true;
    }

    private void loseHealth()
    {
        if(health > 0)
        {
            health --;
            controller.disableCops(copDisableTime);//disables cops for a couple of seconds
        }
        else
        {
            health = 0;
            controller.lost();
        }
        collidDelay = false;
        lightAnimator[health].SetBool("health",false);
        Invoke("reactivateCollider",copDisableTime);
    }
}
