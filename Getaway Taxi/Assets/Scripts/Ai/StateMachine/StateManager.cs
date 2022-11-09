using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private State currentState;//the state thats now active on the AI 

    [Header("Switch from all States")]
    [SerializeField] private CrashedState crashedState;//the state for the vehicle crashing

    [Header("Switch all Values")]
    [SerializeField] private bool crashed = false;//if the AI is crashed

    [Header("Private data")]
    private AiController controllerScript;//the controller on the AI

    public void setStart(AiController newScript)//start function gets called from the controller of the car
    {
        controllerScript = newScript;
    }

    private void Update()
    {
        if(!Values.pauzed)//if the game is not pauzed
        { 
            runStateMachine();
        }
    }

    private void runStateMachine()
    {
        checkAll();//checks states that can switch from any state

        State nextState = currentState?.runThisState();//if not null then run current state other wise dont run

        if(nextState != null)
        {
            SwitchStateNextState(nextState);///switch to the next state
        }
    }

    private void checkAll()//this check can switch from any state
    {
        if(crashed)
        {
            currentState = crashedState; 
        }
    }

    private void SwitchStateNextState(State nextState)//switched to the next state from the last state
    {
        currentState = nextState;
    }


    public bool setCrashed(Vector3 addedForce)//sets the AI to crashed
    {
        if(!crashed)
        {
            crashedState.crash(addedForce);//adds force on to the AI
            controllerScript.crashed();//sets on this controller the crashed bool to true
            crashed = true;
        }
        
        return crashed;
    }
}
