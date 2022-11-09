using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private OptionController optionScript;

    [Header("Setting")]
    [SerializeField] private int optionId = 0;

    [Header("UI")]
    [SerializeField] private Slider optionSlider;
    [SerializeField] private TMPro.TextMeshProUGUI amountText; 

    public void setNewValue()//when slider value is changed
    {
        int sliderValue = (int)optionSlider.value;
        amountText.text = sliderValue + "%";
        optionScript.saveOption(optionId,sliderValue);
    }

    public void setData(int newAmount)//loads values
    {
        amountText.text = newAmount + "%";
        optionSlider.value = newAmount;
    }
}
