using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrashedState : State
{   
    [Header("Crash settings")]

    [Tooltip("Car drag sets the falling speed of the car")]
    [SerializeField] private Vector2 carfallDrag = new Vector2(2,6);//random amount of dragg added on to the RB to make the vehicle fall down slowly

    [Tooltip("Final forward speed boost")]
    [SerializeField] private Vector2 finalBoost = new Vector2(50,100);//random final force added on to the vehicle when crashing

    [Tooltip("Final boost time")]
    [SerializeField] private float boostTime = 2;//the amount of time the car has the final boost

    [Tooltip("Time after crash when car gets removed")]
    [SerializeField] private float removeTime = 10.0f;//the time the car gets removed after crashing

    [Header("Set Components")]

    [Tooltip("Car rigidbody from root object")]
    [SerializeField] private Rigidbody carRb;//the rigidbody of the AI

    [Tooltip("Animator from carbody object")]
    [SerializeField] private Animator carAnim;//the animator of the crashing for giving it a random direction to give force to
    
    [Tooltip("Agent from rootObject")]
    [SerializeField] private NavMeshAgent agent;//the navmesh agend for path finding on the navmesh
    
    [Tooltip("CrashEffect")]
    [SerializeField] private ParticleSystem crashEffect;//the smoke or other effect while crashing

    [Tooltip("endExplotion")]
    [SerializeField] private ParticleSystem explotionEffect;//the explotion particle effect when destroying the car
    
    [Tooltip("Spinning out animation")]
    [SerializeField] private Animation spinOutAnim;//the spinning out animation that rotates object to give swerling out of control effect when crashing

    [Header("Private Data")]
    private float speed;//the current speed of the car going forward
    private float randomAddSpeed;//random set boost speed 
    private float followSpeed = 0.1f;//the follow speed of the rotation effect

    public override State runThisState()//the update function for this state
    {
        if(boostTime > 0 && carRb)
        {
            carRb.AddForce(transform.forward * speed * randomAddSpeed * Time.deltaTime,ForceMode.VelocityChange);//final boost on rigidbody

            Vector3 backAngle = Vector3.Lerp(carRb.transform.eulerAngles, transform.eulerAngles, followSpeed * Time.deltaTime);//lepr to angle of crash animation to give it a fake randomized effect
            carRb.transform.eulerAngles = backAngle;

            boostTime -= 1 * Time.deltaTime;
        }

        return this;//always returns this state because there is no need to switch to another state
    }

    public void crash(Vector3 addedForce)
    {
        if(carRb)//if it still has rb to prevent bug
        {
            carAnim.enabled = false;//turns off the bobbing hover effect
            agent.enabled = false;//disables ai nav movement
            speed = agent.speed;//gets last speed of agent this effects the final boost speed
            randomAddSpeed = Random.Range(finalBoost.x,finalBoost.y);//random final boost speed
            carRb.isKinematic = false;
            carRb.drag = Random.Range(carfallDrag.x,carfallDrag.y);
            followSpeed = Random.Range(0.2f,0.4f);//random speed of spinning out animation
            carRb.AddForce(addedForce,ForceMode.Impulse);
            spinOutAnim.Play();//plays spinning out animation //can be tweeked for better effect
            
            if(crashEffect)//if has final crashing particle effect
            {
                crashEffect.Play();//plays crashing smoke effect
            }

            Invoke("explode",removeTime);//calls clean up explotion timer 
        }
    }

    private void explode()//called from the invoke
    {
        Destroy(carRb);//so the explotion stays in place
        if(explotionEffect)//if has explotion particle effect play that first then destroy the AI after done playing
        {
            explotionEffect.Play();
            Destroy(this.transform.root.gameObject,explotionEffect.main.duration);
        }
        else
        {
            Destroy(this.transform.root.gameObject,1.0f);//destroy the AI after the given time
        }
        carAnim.gameObject.SetActive(false);//turns of the body of the car to show the explotion particle effect
    }
}
