using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string[] endFeedbackText = new string[3];

    [Header("Ui Components")]

    [Tooltip("Text object to the end feedbackText")]
    [SerializeField] TMPro.TextMeshProUGUI topText;

    [Tooltip("Text object to display the score")]
    [SerializeField] TMPro.TextMeshProUGUI scoreText;

    [Tooltip("Ui Animator controlls the scene switch overlay")]
    [SerializeField] Animator uiAnimator;

    [Header("Private data")]

    private int loadScene = 0;//scene id that gets loaded when animation overlay is done

    void Start()
    {
        checkEndState();
    }

    private void Update()
    {
        // if(Input.GetKeyDown(KeyCode.P))
        // {
        //     nextScene(0);
        // }
    }

    private void checkEndState()
    {
        if(Values.busted)
        {
            topText.text = endFeedbackText[0];
        }
        else
        {
            float highScore = 0;
            GameData loadData = Save.loadGameData();
            if(loadData != null)//if data was found set to saved data
            {
                highScore = loadData.getCurrentHigh();
                Save.saveGameData();
            }

            if(Values.score > highScore)//new highscore
            {
                topText.text = endFeedbackText[2];

            }
            else
            {
                topText.text = endFeedbackText[1];
            }
        }
        scoreText.text = Values.score.ToString();
    }

    public void endAnim()
    {
        SceneManager.LoadScene(loadScene);
    }

    public void nextScene(int newScene)
    {
        loadScene = newScene;
        uiAnimator.SetBool("LoadGame",true);
    }
}
