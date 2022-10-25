using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Option[] options;

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
        options[0].setData(musicVolume);
        options[1].setData(mainVolume);
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

        Save.saveSettingData(this);
    }
}
