using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private OptionController optionScript;//the main option controller script

    [Header("Setting")]
    [SerializeField] private int optionId = 0;//the id of the option

    [Header("UI")]
    [SerializeField] private Slider optionSlider;//the slider to get the value of the option
    [SerializeField] private TMPro.TextMeshProUGUI amountText; //the text next to the slider to display the amount

    public void setNewValue()//when slider value is changed
    {
        int sliderValue = (int)optionSlider.value;//gets the current value of the slider
        amountText.text = sliderValue + "%";//sets the value text to the new value
        optionScript.saveOption(optionId,sliderValue);//saves the new value from the slider
    }

    public void setData(int newAmount)//loads values
    {
        amountText.text = newAmount + "%";//sets the value text 
        optionSlider.value = newAmount;//sets the slider to the loaded value
    }
}
