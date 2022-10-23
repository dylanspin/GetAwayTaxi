using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    private int CurrentLevel = 0;

    public GameData(DataController oData)
    { 
        CurrentLevel = oData.getLevel();
    }

    public int getCurrentLevel()
    {
        return CurrentLevel;
    }
}   
