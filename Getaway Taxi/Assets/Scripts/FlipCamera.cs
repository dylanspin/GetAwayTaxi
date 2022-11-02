using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCamera : MonoBehaviour
{
    [SerializeField] private Camera camera;
    public bool flipHorizontal;

    private void Awake () 
    {
        camera = GetComponent<Camera>();
    }

    private void OnPreCull() 
    {
        camera.ResetWorldToCameraMatrix();
        camera.ResetProjectionMatrix();
        Vector3 scale = new Vector3(flipHorizontal ? -1 : 1, 1, 1);
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