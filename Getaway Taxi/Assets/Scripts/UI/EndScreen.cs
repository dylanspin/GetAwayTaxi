using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string[] endFeedbackText = new string[3];//the feedback text displayed at the end 

    [Header("Ui Components")]

    [Tooltip("Text object to the end feedbackText")]
    [SerializeField] TMPro.TextMeshProUGUI topText;//the text componenent for the end feedback text

    [Tooltip("Text object to display the score")]
    [SerializeField] TMPro.TextMeshProUGUI scoreText;//the text component for displaying the score the player got

    [Tooltip("Ui Animator controlls the scene switch overlay")]
    [SerializeField] Animator uiAnimator;//the switching scene overlay animator

    [Header("Private data")]
    private int loadScene = 0;//scene id that gets loaded when animation overlay is done
    private bool goingNext = false;//if overlay is active

    void Start()
    {
        checkEndState();//checks what to display on the UI
    }

    private void checkEndState()
    {
        if(Values.busted)//if the player got busted
        {
            topText.text = endFeedbackText[0];
        }
        else//if the player reached the end of the game
        {
            float highScore = 0;
            GameData loadData = Save.loadGameData();//gets the saved data
            if(loadData != null)//if data was found set to saved data
            {
                highScore = loadData.getCurrentHigh();//sets the saved highscore
                Save.saveGameData();//saves score
            }

            if(Values.score > highScore)//new highscore
            {
                highScore = Values.score;//gets the new highscore
                Save.saveGameData();//saves score

                topText.text = endFeedbackText[2];//sets feedback text
            }
            else
            {
                topText.text = endFeedbackText[1];//sets feedback text
            }
        }

        scoreText.text = Values.score.ToString();//displays the score on the UI
    }

    public void endAnim()//function called at the end of the transition animation
    {
        SceneManager.LoadScene(loadScene);
    }

    public void nextScene(int newScene)//load the given id scene 
    {
        if(!goingNext)//prevents bug
        {
            goingNext = true;
            loadScene = newScene;
            uiAnimator.SetBool("LoadGame",true);//play overlay on UI
        }
    }
}
