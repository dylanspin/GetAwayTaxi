using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUiController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator mainAnim;
    [SerializeField] TMPro.TextMeshProUGUI scoreText;

    [Header("Private data")]
    private bool found = false;

    public void setStart(bool foundData)
    {
        found = foundData;
        //for disabling new game and continue button etc when there is no save data 
        //or to show a intro
    }

    private void loadData()
    {
        float highScore = 0;
        GameData loadData = Save.loadGameData();
        if(loadData != null)//if data was found set to saved data
        {
            highScore = loadData.getCurrentHigh();
        }
        scoreText.text = highScore.ToString();
    }

    public void overLayLoad()
    {
        mainAnim.SetBool("LoadGame",true);
    }

    public void continueGame()
    {
        SceneManager.LoadScene(1);//SceneManager.GetActiveScene().buildIndex + 1
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
