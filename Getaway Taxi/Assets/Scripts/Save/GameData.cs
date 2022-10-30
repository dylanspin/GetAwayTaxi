using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    private int highScore = 0;

    public GameData()
    { 
        highScore = Values.score;
    }

    public int getCurrentHigh()
    {
        return highScore;
    }
}   
