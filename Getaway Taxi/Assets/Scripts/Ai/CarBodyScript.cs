using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBodyScript : MonoBehaviour
{
    /*
        the script on the AI car bodies
    */

    [SerializeField] private GameObject iconObject;//the icon above the car for the minimap
    [SerializeField] private Animator chaseAnim;//the police light animator

    public void setChase(bool active)//turns on the police lights on the police car body if chasing the player
    {
        if(chaseAnim)
        {
            chaseAnim.SetBool("Chase",chaseAnim);
        }
    }

    public void setIcon(bool active)//turns on the icon if the car is on the same height as the player
    {
        if(iconObject)
        {
            iconObject.SetActive(active);
        }
    }
}
