using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingData
{
    [Header("Floats")]
    private int musicVolume = 50;
    private int mainVolume = 50;

    public SettingData(OptionController oData)
    {

    }


    //get value functions
    public int getMusic()
    {
        return musicVolume;
    }

    public int getVolume()
    {
        return mainVolume;
    }
}
