using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushState : State
{
    /*
        Not used
    */
    public ChaseState shaseState;
    public bool canSeePlayer;

    public override State runThisState()
    {
        if(canSeePlayer)
        {
            return shaseState;
        }
        else
        {
            return this;
        }
    }
}
