using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUiController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator mainAnim;

    [Header("Private data")]
    private bool found = false;

    public void setStart(bool foundData)
    {
        found = foundData;
        //for disabling new game and continue button etc when there is no save data 
        //or to show a intro
    }

    public void overLayLoad()
    {
        mainAnim.SetBool("LoadGame",true);
    }

    public void continueGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void settings()
    {
        mainAnim.SetInteger("Page",1);
    }

    public void mainPage()
    {
        mainAnim.SetInteger("Page",0);
    }
}
