using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour
{
    [Header("Scripts")]

    [Tooltip("All individual options")]
    [SerializeField] private Option[] options;

    [Tooltip("All individual options")]
    [SerializeField] private AudioController audioScript;

    [Header("Setting Data")]
    private int musicVolume = 50;
    private int mainVolume = 50;

    private void Start()
    {
        loadData();
    }

    private void loadData()
    {
        SettingData loadData = Save.loadSettingData();
        if(loadData != null)//if data was found set to saved data
        {
            musicVolume = loadData.getMusic();
            mainVolume = loadData.getVolume();
        }
        else//if there is no save found
        {
            Debug.Log("No save data found");
        }

        loadValues();
    }   

    public void loadValues()
    {
        if(options.Length > 0)
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
        case 0: //music setting
            musicVolume = newValue;
            break;
        case 1://
            mainVolume = newValue;
            break;
        default://do nothing
            break;
        } 

        audioScript.setOptions(false,musicVolume,mainVolume);//sets audio options in the main menu
        Save.saveSettingData(this);
    }
}
