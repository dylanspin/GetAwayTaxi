using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUp : MonoBehaviour
{
    [SerializeField] private Animator popUpAnim;

    public void setStatePopUp(bool active)
    {
        popUpAnim.SetBool("Show",active);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
