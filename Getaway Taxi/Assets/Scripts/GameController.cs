using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("End Settings")]

    [Tooltip("Time for the slow motion end effect")]
    [SerializeField] private float slowMoTime = 0.05f;//the time for the slomo effect

    [Tooltip("Min time speed of the end slowmotion")]
    [SerializeField] private float minSlowmo = 0.05f;//the min speed of the time before the slomo effect ends

    [Header("Scripts")]
    [SerializeField] private GameObject uiHelper;//the ui helper that shoots the lazer to interact with the UI for VR
    [SerializeField] private AiManager aiMangerScript;//the manager script of all the AI cars in the game
    [SerializeField] private TimeManager timeScript;//the script that manages the time speed of the game
    [SerializeField] private EndPoint endScript;//the script for the end trigger of the game
    [SerializeField] private FakeSteeringWheel steerinScript;//the steering wheel script on the steering rig
    private CarController carScript;//the main centeralized controller script of the player car

    public void setStart(CarUI uiScript,CarController newCar)//sets start information from the car 
    {
        timeScript.setStart(uiScript,this);//sets start information for the time script
        carScript = newCar;
    }

    public void startCar(bool active)//starts or stops the car when the input is pressed
    {
        uiHelper.SetActive(!active);
        steerinScript.startCar(active);
    }

    public void reachedEnd()//when the player has entered the end trigger
    {
        carScript.setEnd(false);
        StartCoroutine(timeScript.slowlySlowmo(slowMoTime,minSlowmo,1,1));
    }

    public void pauzedGame(bool active)//would check the stuff when the game is pauzed was not really needed in the end
    {
        Debug.Log("Pauzed : " + active);
        Values.pauzed = active;
    }

    /////get script functions

    public TimeManager getTimeScript()
    {
        return timeScript;
    }

    public AiManager getAiManager()
    {
        return aiMangerScript;
    }

    public FakeSteeringWheel getSteering()
    {
        return steerinScript;
    }
}
