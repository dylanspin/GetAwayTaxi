using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Settings")]

    [Tooltip("The speed of the sped up time")]
    [SerializeField] private float speedUpTime = 2f;//the sped up time speed
    
    [Tooltip("The speed of the slowmotion time")]
    [SerializeField] private float slowMoTime = 0.05f;//the slowmo time speed

    [Tooltip("The speed of the normal time")]
    [SerializeField] private float normalTime = 1;//the normal time speed

    [Header("Private data")]
    private bool slowMoActive = false;//if slowmo is active
    private bool pauzed = false;//if pauzed is active
    private bool speedUp = false;//if speed up is active

    [Header("Scripts")]
    private CarUI uiScript;//the incar ui controller script
    private GameController controllerScript;//the game scene controller

    public void setStart(CarUI newScript,GameController newController)
    {
        uiScript = newScript;
        controllerScript = newController;
        checkEndTime(0);//resets time to make sure its at normal speed
    }

    public void pauzeGame(bool active)
    {
        if(active)
        {
            checkEndTime(1);//pauzes the game
        }
        else
        {
            checkEndTime(0);//turns back to normal time
        }
    } 

    public void fasterTime(bool active)
    {
        if(active)
        {
            checkEndTime(3);//speeds up the game
        }
        else
        {
            checkEndTime(0);//turns back to normal time
        }
    }

    public void slowMotion(bool active)
    {
        if(active)
        {
            checkEndTime(2);//turns on slowmotion
        }
        else
        {
            checkEndTime(0);//turns back to normal time
        }
    }
    
    private void checkEndTime(int resetTime)//checks to what time the time speed should be set
    {
        //turns of all bools
        slowMoActive = false;
        pauzed = false;
        speedUp = false;
        if(resetTime == 0)//normal time
        {
            Time.timeScale = normalTime;
            Time.fixedDeltaTime = 0.01388889f;
            controllerScript.pauzedGame(false);
        }
        else if(resetTime == 1)//pauze time
        {
            pauzed = true;
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0.01388889f;
            controllerScript.pauzedGame(true);
        }
        else if(resetTime == 2)//slow mo time
        {
            slowMoActive = true;
            Time.timeScale = slowMoTime;
            Time.fixedDeltaTime = Time.timeScale * 0.01388889f;
            controllerScript.pauzedGame(false);
        }
        else if(resetTime == 3)//speed up time
        {
            speedUp = true;
            Time.timeScale = speedUpTime;
            Time.fixedDeltaTime = 0.01388889f;
            controllerScript.pauzedGame(false);
        }
        //4 = keep same time
    }

    //Slowly changes the speed of time 
    public IEnumerator slowlySlowmo (float duration, float minTimeSpeed,int resetTime,int endUi)
    {
        float startTime = Time.timeScale;//the current time speed when coroutine is caled so it can return to the start time
        float elepsed = 0.0f;//how much time has elepsed
        float currentTime = startTime;//the time value that gets changed over time 

        while(elepsed < duration)
        {
            elepsed += Time.unscaledDeltaTime;//ads time on to the elepsed time value

            currentTime = Mathf.Lerp(currentTime, minTimeSpeed, duration * Time.unscaledDeltaTime);//slowly lerp down the time speed with unscaled time
            Time.timeScale = currentTime;//sets the current time speed to the new one
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            yield return null;
        }

        if(resetTime == 5)//goes back to normal time speed after full slowmotion with the same coroutine basicly in reverse
        {
            StartCoroutine(slowlySlowmo(duration/3,startTime,0,-1));
        }
        else
        {
            checkEndTime(resetTime);//check what time time speed it should be set after the coroutine is over
            uiScript.setEndUITime(endUi);//check if there is UI to be shown after the coroutine is over
        }
    }
}
