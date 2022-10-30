using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtend : MonoBehaviour
{
    [Header("Objects")]
   
    [Tooltip("Transform of the end object")]
    [SerializeField] private Transform endPoint;

    void Start()
    {
        // float yAngle = Vector3.Angle(this.transform.position, endPoint.position);
        // Vector3 defaultRot = transform.eulerAngles;
        // defaultRot.y = yAngle;
        // transform.LookAt(endPoint);
        var rotation = Quaternion.LookRotation(transform.position - endPoint.position);
        {
            transform.rotation = rotation;
        }
        // defaultRot.y = transform.eulerAngles.y;
        // transform.eulerAngles = defaultRot;
    }

}
