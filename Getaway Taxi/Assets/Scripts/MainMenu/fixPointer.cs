using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
     
public class fixPointer : MonoBehaviour
{
    private EventSystem eventSystemComponent;
 
    void Start()
    {
        eventSystemComponent = GetComponentInParent<EventSystem>();
    }
 
    private void OnEnable()
    {
        StartCoroutine(Co_ActivateInputComponent());
    }
 
    private IEnumerator Co_ActivateInputComponent()
    {
        yield return new WaitForEndOfFrame();
        eventSystemComponent.enabled = false;
        yield return new WaitForSeconds(0.2f);
        eventSystemComponent.enabled = true;
    }
}
