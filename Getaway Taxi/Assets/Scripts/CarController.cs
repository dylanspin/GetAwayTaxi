using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{   
    /*
        Centralized script that shares the information with all other scripts
    */

    [Header("End Settings")]

    [Tooltip("Minimum time sped to stop - Time it takes to go to the min value")]
    [SerializeField] private Vector2 endEffect = new Vector2(0.1f,5.0f);//the values for the end screen slomotion effect

    [Header("SteeringWheelObjects")]
    [SerializeField] private Transform steeringWheel;//the visual rotating steering wheel
    [SerializeField] private Transform[] handPos = new Transform[2];//positions of the hand controllers
    [SerializeField] private Transform[] holdPos = new Transform[2];//positions of where the hands be attached to
    [SerializeField] private Transform[] handVis = new Transform[2];//visuals of the vr hand controllers

    [Header("Objects")]
    [SerializeField] private GameObject pcPlayer;//normal pc look player
    [SerializeField] private GameObject vrPlayer;//vr player body

    [Header("Scripts")]
    [SerializeField] private GameController controllerScript;//main scene/game controller script
    [SerializeField] private Car movementScript;//movement of the car script
    [SerializeField] private CarCollider colliderScript;//controls what happens when car collides
    [SerializeField] private CarUI uiScript;//manages the ui of the car
    [SerializeField] private CarStats statScript;//keeps track of the stats of the car
    [SerializeField] private SpecialPowers specialScript;//emp power concept script - not used
    [SerializeField] private CarAudio audioScript;//emp power concept script - not used

    [Header("Private Data")]
    private bool started = false;

    void Start()
    {
        Values.score = 0;//rests the score for the endscreen
        Values.busted = false;//rests the busted state for the endscreen
        setStartData();
        setStartVr();
    }

    private void setStartVr()//turns on or of the pc or vr looking object
    {
        pcPlayer.SetActive(Application.isEditor);
        vrPlayer.SetActive(!Application.isEditor);
    }
   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) || OVRInput.GetDown(OVRInput.Button.One))//check if the start car inputs are pressed
        {
            if(!Values.pauzed)//if game is not pauzed
            {   
                startCar(!started);//stars or stops car
            }
        }
    }

    private void setStartData()//sets the script informations
    {
        controllerScript.getSteering().setStart(steeringWheel,handPos,handVis,holdPos);
        specialScript.setStart(controllerScript,controllerScript.getTimeScript(),uiScript);
        movementScript.setStart(uiScript,controllerScript.getAiManager(),controllerScript.getSteering());
        statScript.setStart(movementScript);
        colliderScript.setStartData(movementScript,statScript,this,audioScript);
        uiScript.setStart(statScript);
        controllerScript.setStart(uiScript,this);
    }

    private void startCar(bool active)//starts the car when the input is pressed
    {
        started = active;//sets start bool
        movementScript.startCar(active);//the movement script of the car
        uiScript.activateCar(active);//the in car ui controller script
        specialScript.setStarted(active);//special power script
        controllerScript.startCar(active);//the game controller car
        audioScript.startCar(active);//the audio script for the car
        //needs to trigger animation that makes the car shake 
    }

    public void returnMain()//when the main menu button is clicked in the car
    {
        SceneManager.LoadScene(0);//loads the main menu scene
    }

    public void disableCops(float timeDisable)//disables all cop cars when collided with one
    {
        controllerScript.getAiManager().disableCops(timeDisable);//disables the cop cars via the ai controller script
    }

    public void lost()//when the game is lost/busted
    {
        setEnd(true);//sets the end values and plays the end sound effect
        StartCoroutine(controllerScript.getTimeScript().slowlySlowmo(endEffect.y,endEffect.x,1,0));//starts end slowmo transition effect
    }

    public void setEnd(bool caught)//sets the end values and plays the end sound effect
    {
        audioScript.playEnd(caught);//plays the end game sound effect
        Values.busted = caught;//sets if busted for end screen
        Values.score = (int)statScript.getScore();//sets the score for the end screen 
    }
}
