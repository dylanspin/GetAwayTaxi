using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtend : MonoBehaviour
{
    /*
        Looks at the end point transform
    */

    [Header("Objects")]
    [Tooltip("Transform of the end object")]
    [SerializeField] private Transform endPoint;//the object to look at 

    void Start()
    {
        var rotation = Quaternion.LookRotation(transform.position - endPoint.position);//gets the difference rotation
        {
            transform.rotation = rotation;//sets the rotation
        }
    }
}
