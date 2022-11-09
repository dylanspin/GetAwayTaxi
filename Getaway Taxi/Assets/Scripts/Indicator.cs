using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator indicatorAnim;//the transition animator 
    [SerializeField] private Indicator behind;//the indicator behind this one

    [Header("Private data")]
    private int countTrigger = 0;//the amount of gameobjects in the trigger
    private bool frontSet = false;//if the indicator in front is set or not
 
    void OnTriggerEnter(Collider other) //when something enters the transition trigger
    {
        if(countTrigger == 0)//if there wasent anything in the trigger
        {
            Debug.Log("Entered");
            if(behind)//if has indcator behind
            {
                behind.setBehind(true);//sets indcator behind
            }
            indicatorAnim.SetBool("Show",true);//turns on this indicator with the animator
        }
        countTrigger ++;
    }
 
    void OnTriggerExit(Collider other) 
    {
        if(countTrigger - 1 > 0)//removes from the intrigger counter
        {
            countTrigger --;
        }
        else{//if there is nothing in the trigger anymore
            countTrigger = 0;
            if(!frontSet)
            {
                if(behind)//if has trigger behind this one
                {
                    behind.setBehind(false);//checks if the trigger behind should be turned of aswell
                }
                indicatorAnim.SetBool("Show",false);//turns of this indicator via the animator
            }
        }
    }

    public void setBehind(bool active)
    {
        frontSet = active;
        if(active)//turns on indicator no check needed
        {
            indicatorAnim.SetBool("Show",true);
        }
        else//tries to turn of indicator 
        {
            if(countTrigger == 0)//if there is nothing in this trigger
            {
                indicatorAnim.SetBool("Show",false);
            }
        }
        if(behind)//check the trigger behind this one if there is one
        {
            behind.setBehind(active);
        }
    }

}
