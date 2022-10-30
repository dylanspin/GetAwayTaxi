using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{   
    [Header("End Settings")]
    [SerializeField] private Vector2 endEffect = new Vector2(0.1f,5.0f);

    [Header("SteeringWheelObjects")]
    [SerializeField] private Transform steeringWheel;
    [SerializeField] private Transform[] handPos = new Transform[2];
    [SerializeField] private Transform[] holdPos = new Transform[2];
    [SerializeField] private Transform[] handVis = new Transform[2];

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

    [Header("Private Data")]
    private bool started = false;

    void Start()
    {
        setStartData();
        // setStartVr();
    }

    private void setStartVr()
    {
        pcPlayer.SetActive(Application.isEditor);
        vrPlayer.SetActive(!Application.isEditor);
    }
   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) || OVRInput.GetDown(OVRInput.Button.One))
        {
            if(!Values.pauzed)
            {   
                startCar(!started);
            }
        }
    }

    private void setStartData()
    {
        controllerScript.getSteering().setStart(steeringWheel,handPos,handVis,holdPos);
        specialScript.setStart(controllerScript,controllerScript.getTimeScript(),uiScript);
        movementScript.setStart(uiScript,controllerScript.getAiManager(),controllerScript.getSteering());
        statScript.setStart(movementScript);
        colliderScript.setStartData(movementScript,statScript,this);
        uiScript.setStart(statScript);
    }

    private void startCar(bool active)
    {
        started = active;
        movementScript.startCar(active);
        uiScript.activateCar(active);
        specialScript.setStarted(active);
        controllerScript.startCar(active);
        //needs to trigger animation that makes the car shake 
    }

    public void disableCops(float timeDisable)
    {

    }

    public void lost()
    {
        StartCoroutine(controllerScript.getTimeScript().slowlySlowmo(endEffect.y,endEffect.x,5));
    }
}
