using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [Header("Hover components")]
   
    [Tooltip("Animator of the button")]
    [SerializeField] private Animator anim;

    [Header("Hover Settings")]

    [Tooltip("Name of animator boolean to switch states")]
    [SerializeField] private string boolName = "Enter";

    public void setState(bool active)
    {
        anim.SetBool(boolName,active);
    }
}
