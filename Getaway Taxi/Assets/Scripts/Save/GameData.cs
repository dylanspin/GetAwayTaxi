using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    private int highScore = 0;//the value for the highscore

    public GameData()
    { 
        highScore = Values.score;//sets the new highscore
    }

    public int getCurrentHigh()//returs the private highscore value
    {
        return highScore;
    }
}   
