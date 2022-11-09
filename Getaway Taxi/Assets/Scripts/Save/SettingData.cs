using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingData
{
    [Header("Floats")]
    private int musicVolume = 50;//the value for the music volume between 0 - 100
    private int mainVolume = 50;//the value for the main overall volume between 0 - 100

    public SettingData(OptionController oData)
    {
        //sets the data fom the optioncontroller
        musicVolume = oData.getMusic();
        mainVolume = oData.getMainSound();
    }

    //get value functions
    public int getMusic()//returns the private music volume value
    {
        return musicVolume;
    }

    public int getVolume()//returns the private main volume value
    {
        return mainVolume;
    }
}
