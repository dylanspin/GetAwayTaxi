using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [Header("Hover components")]
   
    [Tooltip("Animator of the button")]
    [SerializeField] private Animator anim;//the animator on the ui for the hover effect

    [Header("Hover Settings")]

    [Tooltip("Name of animator boolean to switch states")]
    [SerializeField] private string boolName = "Enter";//the bool name for the animator 

    public void setState(bool active)//called from the even componenent 
    {
        anim.SetBool(boolName,active);//sets the bool on the animator for the hover animation
        if(active)//if enter then check if it can play button sound effect
        {
            if(this.transform.root.GetComponent<AudioController>())
            {
                this.transform.root.GetComponent<AudioController>().playButtonEffect();//play button sound effect
            }
        }
    }
}
