using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    // [Header("Hover Settings")]
        
    // [Tooltip("Min hover height and Max hover height")]
    // [SerializeField] private Vector2 hoverHeights = new Vector2(0.6f,5.0f);
    // [SerializeField] private float[] hoverHeights = new float[4];

    // [Tooltip("The speed the car goes up and down in height")]
    // [SerializeField] private float heighChangeSpeed = 2.5f;

    [Header("Car Movement Speed:")]

    [Tooltip("Max Car Speed - Forward - Backwards")]
    [SerializeField] private Vector2 maxSpeed = new Vector2(50,30);//the max for ward and backwards speed

    [Tooltip("Engine Forward Speed")]
    [SerializeField] private float accelerateSpeed = 50;//the speed the car adds forward acceleration

    [Tooltip("Engine Reverse speed")]
    [SerializeField] private float reverseSpeed = 25;//the speed the car adds backwards acceleration 

    [Tooltip("When acceleration is above 0 the multiplier when breaking so the car stops faster")]
    [SerializeField] private float breakMultiplier = 4;//the multiplier of the reverse speed to stop the car faster if the acceleration is above 0

    [Tooltip("Engine losing speed")]
    [SerializeField] private float loseSpeed = 25;//the amount of acceleration that gets lost over time when there is no input

    [Header("VR inputs")]
    [SerializeField] private OVRInput.Button gasInputs;//the vr input for giving gas
    [SerializeField] private OVRInput.Button reverseInputs;//the vr input for reverse

    [Header("Steering Wheel settings")]

    [Tooltip("Speed of what the car turns at based on the steeringwheel angle")]
    [SerializeField] private float turnSpeed = 100;//multiplier the car turns

    [Header("Set Data")]

    [Tooltip("The transform of where the object gets pushed")]
    [SerializeField] private Transform trustPos;//position where the car gets boosted at

    [Tooltip("Rigidbody of car gameobject")]
    [SerializeField] private Rigidbody carRb;//the rigidbody for the car

    [Header("Private Scripts")]
    private CarUI carUIScript;//the in car ui controller script
    private AiManager aiScript;//the manager of all the ai in the scene
    private FakeSteeringWheel steerinScript;//the steering wheel script

    [Header("Private Data")]
    private float acelleration = 0;//the current acelleration of the car

    //not used anymore
    // private float defaultHeight;
    // private int currentHeight = 0;
    // private int lastHeight = 0;

    private bool started = false;//if the car has started

    /*
        Controls for now to test with pc : 

        a-d to steer left to right hold space to "hold" the steering weel
        w-s to go up and down 
        left mouse button to accelerate forwards
        right mouse button to accelerate backwards
        r to start the car

        controlls will be maped to controller of the vr headset
    */
    
    public void setStart(CarUI newUi,AiManager newManager,FakeSteeringWheel newSteering)
    {
        carUIScript = newUi;
        aiScript = newManager;
        // defaultHeight = transform.position.y;
        steerinScript = newSteering;
    }

    void Update()
    {
        if(!Values.pauzed)
        { 
            if(started)
            {
                accelerate();//acelerating forward or backwards function
                steering();//steering left and right
                // CheckChangeHeight();
            }
            // goToHeight();
        }
    }

    private void accelerate()
    {
        bool inputForward = OVRInput.Get(gasInputs) || Input.GetKey(KeyCode.W);//gets gass input
        bool inputReverse = OVRInput.Get(reverseInputs) || Input.GetKey(KeyCode.S);//gets reverse/break input

        float gass;
        gass = (inputForward ? 1:0 + - (inputReverse ? 1:0));

        carUIScript.setCamera(gass);
        addGass(gass);

        carRb.AddForceAtPosition(trustPos.forward * acelleration * Time.deltaTime,trustPos.position,ForceMode.VelocityChange);//moves car forward
    }

    private void addGass(float gass)
    {
        if(gass == 0)//if no input
        {
            acelleration = returnZero(acelleration,loseSpeed);//returns the acceleration to 0 when there is no input
        }
        else//if has input
        {
            float multiplier = ((gass < 0 && acelleration > 0) ? breakMultiplier : 1);//calculates the multiplier for the speed
            float carSpeed = gass > 0 ? accelerateSpeed : reverseSpeed;//gets the car direction speed

            acelleration += gass * carSpeed * multiplier * Time.deltaTime; //adds acceleration 
            acelleration = Mathf.Clamp(acelleration,-maxSpeed.y,maxSpeed.x);//clamps accelaration
        }  
    }

    private void steering()
    {
        transform.Rotate(Vector3.up * steerinScript.procentageAngle() * Time.deltaTime * turnSpeed);//rotates car in steering direction
        transform.localEulerAngles = new Vector3(0,transform.localEulerAngles.y,0);//sets new angle 
    }

    private float returnZero(float currentAmount, float returnSpeed)//return value to 0 with the given amount per call
    {   
        float addAmount = returnSpeed * Time.deltaTime;
       
        if(currentAmount > 0)
        {
            if(currentAmount - addAmount > 0)
            {
                currentAmount -= addAmount;
            }
            else
            {
                currentAmount = 0; 
            }
        }
        else if(currentAmount < 0)
        {
            if(currentAmount + addAmount < 0)
            {
                currentAmount += addAmount;
            }
            else
            {
                currentAmount = 0; 
            }
        }

        return currentAmount;
    }

    //outside script functions
    public void collision(float amount)//when the car collided with AI
    {
        acelleration = returnZero(acelleration,amount * 100);
    }

    public void startCar(bool active)//starts the car
    {
        started = active;

        //noted used anymore used to change the height when the car started
        // if(active)
        // {
        //     changeHeight(1);
        // }
        // else
        // {
        //     changeHeight(0);
        // }
    }

    ///not used anymore

    // private void CheckChangeHeight()
    // {
    //     if(Mathf.Abs(defaultHeight+hoverHeights[currentHeight]-transform.position.y) < 0.3f)
    //     {
    //         if(Input.GetMouseButton(0))
    //         {
    //             if(currentHeight < hoverHeights.Length-1)
    //             {
    //                 changeHeight(currentHeight + 1);
    //             }
    //         }
    //         else if(Input.GetMouseButton(1))
    //         {
    //             if(currentHeight > 0)
    //             {
    //                 changeHeight(currentHeight - 1);
    //             }
    //         }
    //     }
    // }

    // private void changeHeight(int newHeight)
    // {
    //     Values.heightLayer = newHeight;//for new spawned cars
    //     lastHeight = currentHeight;
    //     aiScript.setHeight(currentHeight);
    //     if(newHeight > currentHeight)
    //     {
    //         dir = 1;
    //     }
    //     else
    //     {
    //         dir = -1;
    //     }
    //     currentHeight = newHeight;
    // }

    // private void goToHeight()
    // {
    //     transform.Translate(Vector3.up * dir * heighChangeSpeed * Time.deltaTime);
    //     Vector3 originalPos = transform.position;
    //     float setHeight;
    //     if(dir > 0)
    //     {
    //         setHeight = Mathf.Clamp(originalPos.y, defaultHeight+hoverHeights[lastHeight], defaultHeight+hoverHeights[currentHeight]);    
    //     }
    //     else
    //     {
    //         setHeight = Mathf.Clamp(originalPos.y, defaultHeight+hoverHeights[currentHeight], defaultHeight+hoverHeights[lastHeight]);
    //     }
    //     originalPos.y = setHeight;
    //     transform.position = originalPos;
    // }

    ////////////////////// get values

    public float getSpeed()//returns the speed of the car so it can be displayed on the UI
    {
        return carRb.velocity.magnitude;
    }

    public float getAccel()//returns the acceleration of the car so it can be displayed on the UI
    {
        return acelleration;
    }
}
