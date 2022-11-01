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
    private Car carScript;

    [Header("Private Stats")]
    private float distanceMoved = 0.0f;
    private float time = 0;

    public void setStart(Car newMovement)
    {
        carScript = newMovement;
    }
    
    private void Update()
    {
        countDistance();
        time += 1 * Time.deltaTime;
    }

    private void countDistance()
    {
        Vector3 distanceVector = transform.position - oldPos;
        float distanceThisFrame = distanceVector.magnitude;
        distanceMoved += distanceThisFrame;
        oldPos = transform.position;
    }

    ////////////// get data : 

    public float getMovedDistance()
    {
        return distanceMoved;
    }

    public float getAccel()
    {
        return carScript.getAccel();
    }

    public float getSpeed()
    {
        return carScript.getSpeed();
    }

    public float getTime()
    {
        return time;
    }

}
