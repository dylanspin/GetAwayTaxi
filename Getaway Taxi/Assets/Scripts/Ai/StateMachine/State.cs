using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract State runThisState();//returns the state thats given from the overide fucntion
}
