using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Right_Long_Press : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        OnRightPressEvent onRightPressEvent = new OnRightPressEvent();


        CallbackEventSystem.Current.FireEvent(onRightPressEvent);
        Debug.Log("Pointer Down Right Long Press Event");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnRightReleaseEvent onRightReleaseEvent = new OnRightReleaseEvent();

        CallbackEventSystem.Current.FireEvent(onRightReleaseEvent);

        Debug.Log("Pointer Up Right Long Press Event");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
