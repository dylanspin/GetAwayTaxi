using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignition : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private MainUiController uiScript;//the main ui controller of the main menu

    [Header("Components")]

    [Tooltip("The hand image thats shown when input is held")]
    [SerializeField] private GameObject handImage;//the image of the hand on the ignition when the input is held down

    [Header("VR Components")]
    [SerializeField] private OVRInput.Button grabInput;//the input for grabbing the ignition

    [Tooltip("The right hand controller")]
    [SerializeField] private Transform hand;//the right hand controller transform

    [Header("Settings")]

    [Tooltip("Time ignition angle returns to 0 when let go")]
    [SerializeField] private float returnTime = 2;//the time it takes for the ignition to return to the 0 angle

    [Tooltip("Distance between max rotation and rotation needed to start the game")]
    [SerializeField] private float minDistance = 5;//min distance from the max rotation to start the game

    [Header("Private data")]
    private Vector3 lastRot;//the current rotation
    private Quaternion heldRot;//the start rotation when first held

    private void Start()
    {
        heldRot = hand.localRotation;
    }

    private void Update()
    {
        if(OVRInput.GetDown(grabInput))//checks if grabinput is down 
        {
            heldRot = hand.localRotation;//sets the start rotation
        }

        bool getInput = OVRInput.Get(grabInput);//checks if grabinput is held down 
        handImage.SetActive(getInput);//turns on the hand image depening if the input is held down

        if(getInput)//trigger hold
        {
            lastRot = hand.localEulerAngles;//only uses the z angle
            lastRot.x = 0;
            lastRot.y = 0;
            checkRotated();//check if the hand is rotated enough
        }
        else
        {
            float zAngle = lastRot.z;//gets the current z angle
            zAngle = Mathf.Lerp(zAngle,0,returnTime * Time.deltaTime);//lerps back to 0 angle
            lastRot.z = zAngle;//sets the new zAngle
        }
        
        // setRotClamp();

        transform.localEulerAngles = lastRot;//sets the rotation of the ignition
    }

    private void checkRotated()//check if the ignition rotated enough
    {
        float angle = Quaternion.Angle(hand.localRotation, heldRot);//get angle between start and current angle
        float difference = Mathf.Abs(90 - angle);//get absolute difference between max 90 degree angle and the current one
        if(difference < minDistance)
        {
            uiScript.overLayLoad();//starts the game 
        }
    }

    // private void setRotClamp()//caused issues 
    // {
    //     if(lastRot.z > 180)
    //     {
    //         lastRot.z = 0;
    //     }
    //     else if(lastRot.z > 90)
    //     {
    //         lastRot.z = 90;
    //     }
    // }
}
