using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Forward_Long_Press : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        OnForwardPressEvent onForwardPressEvent = new OnForwardPressEvent();

        Debug.Log(CallbackEventSystem.Current);

        CallbackEventSystem.Current.FireEvent(onForwardPressEvent);
        Debug.Log("Pointer Down Event");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnForwardReleaseEvent onForwardReleaseEvent = new OnForwardReleaseEvent();

        CallbackEventSystem.Current.FireEvent(onForwardReleaseEvent);

        Debug.Log("Pointer Up Event");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
