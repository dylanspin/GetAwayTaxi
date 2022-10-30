using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    /*
        Not used anymore now we dont want to have save levels
    */
    
    [Header("Scripts Data")]
    [SerializeField] MainUiController uiScript;

    [Header("Private Data")]
    private int highScore = 0;

    void Start()
    {
        loadData();
    }

    private void loadData()
    {
        GameData loadData = Save.loadGameData();
        if(loadData != null)//if data was found set to saved data
        {
            highScore = loadData.getCurrentHigh();
            uiScript.setStart(true);
        }
        else//if there is no save found
        {
            uiScript.setStart(false);
            Debug.Log("No save data found");
        }
    }


    //get fuctions

    public int getCurrentHigh()
    {
        return highScore;
    }
}
