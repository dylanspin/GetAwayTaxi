using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCamera : MonoBehaviour
{
    /*Not used anymore used to be for flipping the image of the car mirrors but doesnt work anymore in URP*/

    [SerializeField] private Camera camera;
    [SerializeField] private bool flipHorizontal;

    private void Awake () 
    {
        OnPreCull();
        OnPreRender();
        OnPostRender();
    }

    private void OnPreCull() 
    {
        camera.ResetWorldToCameraMatrix();
        camera.ResetProjectionMatrix();
        Vector3 scale = new Vector3(flipHorizontal ? -1 : 1, -1, 1);
        camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(scale);
    }

    private void OnPreRender() 
    {
        GL.invertCulling = flipHorizontal;
    }
     
    private void OnPostRender() 
    {
        GL.invertCulling = false;
    }
}
