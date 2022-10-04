using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Left_Long_Press : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        OnLeftPressEvent onLeftPressEvent = new OnLeftPressEvent();


        CallbackEventSystem.Current.FireEvent(onLeftPressEvent);
        Debug.Log("Pointer Down Left Long Press Event");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnLeftReleaseEvent onLeftReleaseEvent = new OnLeftReleaseEvent();

        CallbackEventSystem.Current.FireEvent(onLeftReleaseEvent);

        Debug.Log("Pointer Up Left Long Press Event");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
