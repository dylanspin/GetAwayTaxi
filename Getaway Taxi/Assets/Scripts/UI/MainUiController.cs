using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUiController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator mainAnim;//the main animator for switching to pages
    [SerializeField] TMPro.TextMeshProUGUI scoreText;//the highscore text to display the current highscore on the main menu

    public void setStart(bool foundData)
    {
        //resets the time scales
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.01388889f;

        // if(foundData)//for displaying information when the game is played for the first time ever 
        // {

        // }
    }

    private void loadData()//loads the data to get the current highschool
    {
        float highScore = 0;
        GameData loadData = Save.loadGameData();
        if(loadData != null)//if data was found set to saved data
        {
            highScore = loadData.getCurrentHigh();//get the current highscore from the save file
        }

        scoreText.text = highScore.ToString();//set the text to the highscore
    }

    public void overLayLoad()//load overlay to switch scenes
    {
        mainAnim.SetBool("LoadGame",true);
    }

    public void continueGame()//goes to the loading screen
    {
        SceneManager.LoadScene(1);//SceneManager.GetActiveScene().buildIndex + 1
    }

    public void settings()//goes to the setting page 
    {
        mainAnim.SetInteger("Page",1);
    }

    public void mainPage()//goes to the main page
    {
        mainAnim.SetInteger("Page",0);
    }
}
