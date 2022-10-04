using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Backward_Long_Press : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        OnBackwardPressEvent onBackwardPressEvent = new OnBackwardPressEvent();

        Debug.Log(CallbackEventSystem.Current);

        CallbackEventSystem.Current.FireEvent(onBackwardPressEvent);
        Debug.Log("Pointer Down Event");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnBackwardReleaseEvent onBackwardReleaseEvent = new OnBackwardReleaseEvent();

        CallbackEventSystem.Current.FireEvent(onBackwardReleaseEvent);
        Debug.Log("Pointer Up Event");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
