using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{

    [Header("Objects")]

    [Tooltip("Reverse camera")]
    [SerializeField] private GameObject[] reverseCamera;//Screens on the right side of the player that has: mini map - Reverse camera - blank
    [SerializeField] private float turnOffTime = 1.5f;//time to turn of the reverse camera after going forward again

    [Header("End Screen Overlay")]
    [SerializeField] private Animator endOverlay;//end screen overlay animator
    [SerializeField] private TMPro.TextMeshProUGUI endMessage;//end message text object
    [SerializeField] private string[] endText = {"Busted","Escaped!"};//To display on the overlay when either busted or reached the end

    [Header("Ui objects")]

    [Tooltip("Indicator to show the energy of the car")]
    [SerializeField] private Slider energySlider;///not used anymore was planned to be used for the EMP effect

    [Tooltip("Text object of the speed counter")]
    [SerializeField] private TMPro.TextMeshProUGUI speedCounter;//the text object for displaying the speed of the car

    [Tooltip("Text object of the acceleration")]
    [SerializeField] private TMPro.TextMeshProUGUI accelearation;//the text object for displaying the accelaration of the car

    [Tooltip("Text object of the moved distance counter")]
    [SerializeField] private TMPro.TextMeshProUGUI movedDistance;//the text object for displaying the moved distance

    [Tooltip("Start ui thats displayed on the screen in the car")]
    [SerializeField] private GameObject startUi;//start ui screen that gives some instructions and allows the player to go back to the main menu

    [Header("Private Scripts")]
    private CarStats statsScript;//stats scripts a centeralized script for keeping track of the current car stats

    [Header("Private data")]
    private bool carActive = false;//if car is started
    private int gearArrayID = 1;//direction of the car 
    private string[] directions = {"R","N","F"};//the Letters infront of the speed to indicate what direction the car is going

    public void setStart(CarStats newStats)
    {
        statsScript = newStats;//sets the stats script from the car controller script
    }

    private void Update()
    {
        if(!Values.pauzed)//if game is not pauzed
        {
            if(carActive)//if the game is started
            {
                setUI();//updates the UI
            }
        }
    }

    public void activateCar(bool active)
    {
        carActive = active;
        setScreenActive(active);//sets the minimap and reverse camera active
        startUi.SetActive(!active);//ui for going back to main menu
        
        if(!carActive)//when car is turned off
        {
            CancelInvoke("turnOffReverse");
        }
    }

    public void setCamera(float gear)//sets the direction of the car
    {
        gearArrayID = Mathf.RoundToInt(gear)+1;

        if(gear != -1)
        {
            if(reverseCamera[0].activeSelf && !IsInvoking("turnOffReverse"))//if the reverse camera isnt already being turned off
            {
                Invoke("turnOffReverse",turnOffTime);
            }
        }
        else
        {
            CancelInvoke("turnOffReverse");
            reverseCamera[0].SetActive(true);
            reverseCamera[1].SetActive(false);
        }
    }

    public void setScreenActive(bool active)//sets the mini map active
    {
        reverseCamera[1].SetActive(active);
        reverseCamera[2].SetActive(!active);
    }

    public void turnOffReverse()//turns of the reverse camera to the mini map again
    {
        reverseCamera[0].SetActive(false);
        reverseCamera[1].SetActive(true);
    }   

    private void setUI()//sets the UI data in the car
    {
        // movedDistance.text = "M: " + statsScript.getMovedDistance().ToString("F0");
        movedDistance.text = statsScript.getScore().ToString("f2");//sets the score counter on the right side of the player
        speedCounter.text = directions[gearArrayID] + statsScript.getSpeed().ToString("F1");//sets the speed of the car with the current direction letter
        accelearation.text = statsScript.getAccel().ToString("F1");//sets the acceleration text
    }

    public void setEndUITime(int showId)//checks at the end of slowmotion effect if there should be UI shown
    {
        endMessage.text = endText[showId];
        endOverlay.SetBool("End",true);
    }

    public void setMaxSlider(float maxAmount,float currentAmount)//gets called from the special script sets the starting data for the slider
    {
        energySlider.maxValue = maxAmount;
        setEnergy(currentAmount);
    }

    public void setEnergy(float newValue)//sets the value on the EMP slider UI
    {
        energySlider.value = newValue;
    }
}
