using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStats : MonoBehaviour
{
    /*
        centralized script to get current car information
    */

    [Header("Private Data")]
    private Vector3 oldPos; //used for getting distance moved

    [Header("Private Script")]
    private Car carScript;//used for getting the speed of the car

    [Header("Private Stats")]
    private float distanceMoved = 0.0f;//counts distance moved
    private float time = 0;//counts the time spend during the chase 

    public void setStart(Car newMovement)
    {
        carScript = newMovement;
    }
    
    private void Update()
    {
        // countDistance();//counts the distance moved
        time += 1 * Time.deltaTime;//adds time to the counter
    }

    // private void countDistance()
    // {
    //     Vector3 distanceVector = transform.position - oldPos;
    //     float distanceThisFrame = distanceVector.magnitude;
    //     distanceMoved += distanceThisFrame;
    //     oldPos = transform.position;
    // }

    ////////////// get data functions 

    // public float getMovedDistance()
    // {
    //     return distanceMoved;
    // }

    public float getAccel()//returns the acceleration of the car
    {
        return carScript.getAccel();
    }

    public float getSpeed()//returns the speed of the car
    {
        return carScript.getSpeed();
    }

    public float getScore()//returns the time score
    {
        return time;
    }
}
