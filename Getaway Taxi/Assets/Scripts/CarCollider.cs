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
    [SerializeField] private float copDisableTime = 2.5f;
    
    [Header("Components")]
    
    [Tooltip("RigidBody")]
    [SerializeField] private Rigidbody carRb;

    [Tooltip("Lights to indicate health")]
    [SerializeField] MeshRenderer[] lightRenderes;

    [Header("Private scripts")]
    private CarStats statsScript;
    private Car carMovementScript;
    private CarController controller;

    [Header("Private Data")]
    private int health = 3;

    public void setStartData(Car newMovement,CarStats newStats,CarController controllerScript)
    {
        carMovementScript = newMovement;
        statsScript = newStats;
        controller = controllerScript;
        health = lightRenderes.Length;
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
                    loseHealth();
                    carRb.AddForce(transform.forward * -statsScript.getSpeed() * Random.Range(backwardsForce.x,backwardsForce.y));
                }
            }

            carMovementScript.collision(statsScript.getSpeed());
        }
    }

    private void loseHealth()
    {
        if(health > 1)
        {
            controller.lost();
        }
        else
        {
            controller.disableCops(copDisableTime);//disables cops for a couple of seconds
        }
    }
}
