using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingData
{
    [Header("Bools")]
    public bool musicOn = true;
    public bool ambientOn = true;
    public bool soundOn = true;
    public bool tips = true;

    [Header("Floats")]
    public float musicVolume = 50;
    public float mainVolume = 50;

    public SettingData(OptionController oData)
    {

    }
}
