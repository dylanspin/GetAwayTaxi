using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPoint : MonoBehaviour
{
   [SerializeField] private Transform[] nextPoints;//the next possible positions the ai can go to 
   [SerializeField] private Color drawLine = Color.blue;//the draw color of the line
   [SerializeField] private bool drawIndicator = false;//if it should draw a white sphere on the position to indicate the point

    public Transform nextPoint()//called when this postion is reached
    {
        return nextPoints[Random.Range(0,nextPoints.Length)];//returns random next point 
    }


    //for debuging the directions and making the roads enable this
    // void OnDrawGizmosSelected()
    // {
    //     if(drawIndicator)
    //     {
    //         Gizmos.color = Color.white;
    //         Gizmos.DrawSphere(transform.position,5);
    //     }
    //     if(nextPoints.Length > 0)
    //     {
    //         for(int i=0; i<nextPoints.Length; i++)
    //         {
    //             if(nextPoints[i] == null)
    //             {
    //                 Gizmos.color = Color.yellow;
    //                 Gizmos.DrawSphere(transform.position,5);
    //             }
    //             else
    //             {
    //                 Vector3 dir = nextPoints[i].position - transform.position;
    //                 float dist = Mathf.Clamp(Vector3.Distance(transform.position, nextPoints[i].position), 0, 10);
    //                 Vector3 newEndpos = transform.position + (dir.normalized * dist);
    //                 Vector3 drawStart = transform.position;
    //                 drawStart.y += 1;
    //                 newEndpos.y += 1;

    //                 Gizmos.color = Color.red;
    //                 Gizmos.DrawLine(drawStart,newEndpos);

    //                 Gizmos.color = drawLine;
    //                 Gizmos.DrawLine(transform.position,nextPoints[i].position);
    //             }
    //         }
    //     }
    //     else
    //     {
    //         Gizmos.color = Color.yellow;
    //         Gizmos.DrawSphere(transform.position,5);
    //     }
    // }
}
