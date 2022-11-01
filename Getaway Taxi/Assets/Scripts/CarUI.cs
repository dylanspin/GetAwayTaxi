using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{
    // [Header("Components")]

    [Header("Objects")]

    [Tooltip("Reverse camera")]
    [SerializeField] private GameObject[] reverseCamera;
    // [SerializeField] private GameObject reverseCamera;
    [SerializeField] private float turnOffTime = 1.5f;

    [Header("End Screen Overlay")]
    [SerializeField] private Animator endOverlay;
    [SerializeField] private TMPro.TextMeshProUGUI endMessage;
    [SerializeField] private string[] endText = {"Busted","Escaped!"};

    [Header("Ui objects")]

    [Tooltip("Indicator to show the energy of the car")]
    [SerializeField] private Slider energySlider;

    [Tooltip("Text object of the speed counter")]
    [SerializeField] private TMPro.TextMeshProUGUI speedCounter;

    [Tooltip("Text object of the acceleration")]
    [SerializeField] private TMPro.TextMeshProUGUI accelearation;

    [Tooltip("Text object of the moved distance counter")]
    [SerializeField] private TMPro.TextMeshProUGUI movedDistance;

    [Tooltip("Start ui thats displayed on the screen in the car")]
    [SerializeField] private GameObject startUi;

    [Header("Private Scripts")]
    private CarStats statsScript;

    [Header("Private data")]
    private bool carActive = false;
    private int gearArrayID = 1;
    private string[] directions = {"R","N","F"};

    public void setStart(CarStats newStats)
    {
        statsScript = newStats;
    }

    private void Update()
    {
        if(!Values.pauzed)
        {
            if(carActive)
            {
                setUI();
            }
        }
    }

    public void activateCar(bool active)
    {
        carActive = active;
        setScreenActive(active);//sets the minimap and reverse camera active
        startUi.SetActive(!active);//ui for going back to main menu
    }

    public void setScreenActive(bool active)
    {
        reverseCamera[1].SetActive(active);
        reverseCamera[2].SetActive(!active);
    }

    public void setCamera(float gear)
    {
        gearArrayID = Mathf.RoundToInt(gear)+1;

        if(gear != -1)
        {
            if(reverseCamera[0].activeSelf && !IsInvoking("turnOffReverse"))
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

    public void turnOffReverse()
    {
        reverseCamera[0].SetActive(false);
        reverseCamera[1].SetActive(true);
    }   

    private void setUI()
    {
        // movedDistance.text = "M: " + statsScript.getMovedDistance().ToString("F0");
        movedDistance.text = statsScript.getScore().ToString("f2");
        speedCounter.text = directions[gearArrayID] + statsScript.getSpeed().ToString("F1");
        accelearation.text = statsScript.getAccel().ToString("F1");
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

    public void setEnergy(float newValue)
    {
        energySlider.value = newValue;
    }
}
