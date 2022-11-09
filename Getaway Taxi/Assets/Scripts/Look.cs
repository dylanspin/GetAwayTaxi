using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{   
    /*
        Look script for on pc 
    */

    [Header("Private Set Data")]
    [SerializeField] private float sensitivity = 1.5f;//sens for looking around
    [SerializeField] private Transform playerBody;//the horizontal rotation object
    [SerializeField] private float minAngle = -65;//the min down angle
    [SerializeField] private float maxAngel = 90;//the max up angle

    [Header("Private data")]
    private float xRotation = 0.0f;//the current x angle Rotation
    private float yRotation = 0.0f;//the current Y angle Rotation
    
    void Update ()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;//the X angle mouse input
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;//the Y angle mouse input

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minAngle, maxAngel);//clamps up and down angle

        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);//rotates this object for looking up and down
        playerBody.Rotate(Vector3.up * mouseX);//rotates the horizontal player object
    }
}
