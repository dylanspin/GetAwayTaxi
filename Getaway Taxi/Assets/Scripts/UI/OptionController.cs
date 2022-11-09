using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour
{
    [Header("Scripts")]

    [Tooltip("All individual options")]
    [SerializeField] private Option[] options;//all the individual options

    [Tooltip("All individual options")]
    [SerializeField] private AudioController audioScript;//the audio controller script
    [SerializeField] private MainUiController uiScript;//the ui controller script for the main menu

    [Header("Setting Data")]
    private int musicVolume = 50;//the value of the music volume
    private int mainVolume = 50;//the value of the main volume

    private void Start()
    {
        loadData();//loads the saved data
    }

    private void loadData()
    {
        SettingData loadData = Save.loadSettingData();//gets the saved data 
        if(loadData != null)//if data was found set to saved data
        {
            //sets the values to the loaded data
            musicVolume = loadData.getMusic();
            mainVolume = loadData.getVolume();
            if(uiScript)//if has ui script say if this is not the first time playing the game
            {
                uiScript.setStart(true);
            }
        }
        else//if there is no save found
        {
            if (uiScript)//if has ui script say if this is the first time playing the game
            {
                uiScript.setStart(false);
            }
        }

        loadValues();//sets the loadded values on scripts
    }   

    public void loadValues()
    {
        if(options.Length > 0)//sets the individual option values
        {
            options[0].setData(musicVolume);
            options[1].setData(mainVolume);
        }

        audioScript.setOptions(true,musicVolume,mainVolume);//sets audio options in the main menu
    }

    public void saveOption(int option, int newValue)
    {
        switch(option) 
        {
            case 0: //music volume setting
                musicVolume = newValue;
                break;
            case 1://main volume setting
                mainVolume = newValue;
                break;
            default://do nothing
                break;
        } 

        audioScript.setOptions(false,musicVolume,mainVolume);//sets audio options in the main menu
       
        Save.saveSettingData(this);//save new values
    }


    public int getMusic()//returns the music volume value
    {
        return musicVolume;
    }

    public int getMainSound()//returns the main volume value
    {
        return mainVolume;
    }
}
