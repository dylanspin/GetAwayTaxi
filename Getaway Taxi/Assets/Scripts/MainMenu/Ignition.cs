using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignition : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private MainUiController uiScript;

    [Header("Components")]

    [Tooltip("The hand image thats shown when input is held")]
    [SerializeField] private GameObject handImage;

    [Header("VR Components")]
    [SerializeField] private OVRInput.Button grabInput;

    [Tooltip("Time ignition angle returns to 0 when let go")]
    [SerializeField] private Transform hand;

    [Header("Settings")]

    [Tooltip("Time ignition angle returns to 0 when let go")]
    [SerializeField] private float returnTime = 4;

    [Tooltip("Distance between max rotation and rotation needed to start the game")]
    [SerializeField] private float minDistance = 5;

    [Header("Private data")]
    private Vector3 lastRot;

    [SerializeField] bool holding = true;

    void Update()
    {
        bool getInput = OVRInput.Get(grabInput);
        handImage.SetActive(getInput);

        if(holding)//trigger hold
        {
            lastRot = hand.localEulerAngles;
            lastRot.x = 0;
            lastRot.y = 0;
            checkRotated();
        }
        else
        {
            float zAngle = lastRot.z;
            zAngle = Mathf.Lerp(zAngle,0,returnTime * Time.deltaTime);
            lastRot.z = zAngle;
        }
        
        setRotClamp();

        transform.localEulerAngles = lastRot;
    }

    private void checkRotated()
    {
        float difference = Mathf.Abs(90 - lastRot.z);
        if(difference < minDistance)
        {
            uiScript.continueGame();
        }
    }

    private void setRotClamp()
    {
        if(lastRot.z > 180)
        {
            lastRot.z = 0;
        }
        else if(lastRot.z > 90)
        {
            lastRot.z = 90;
        }
    }
}
