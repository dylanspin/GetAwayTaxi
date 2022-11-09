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
        Debug.Log(Time.timeScale);
        Debug.Log(Time.fixedDeltaTime);

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.01388889f;

        found = foundData;
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

    private void Update()
    {
        // if(Input.GetKeyDown(KeyCode.P))
        // {
        //     overLayLoad();
        // }
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
