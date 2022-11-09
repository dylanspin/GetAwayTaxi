using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] GameController controllerScript;//the game controller that manages the whole game

    [Header("Private data")]
    private bool ended = false;//bool for if the game has ended
 
    private void OnTriggerEnter(Collider other)//if trigger is entered
    {
        if(!ended)//if the game hasent ended
        {
            if(other.transform.root.tag == "Player")//if object that enters the trigger is the player
            {
                end(true);//sets the ended game true
            }
        }
    }

    public void end(bool active)
    {
        ended = active;
        if(ended)
        {
            controllerScript.reachedEnd();//tell the controller has ended 
        }
    }
}
