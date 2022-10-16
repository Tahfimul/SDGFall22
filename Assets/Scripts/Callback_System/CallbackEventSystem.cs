using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CallbackEventSystem : MonoBehaviour
{
    static private CallbackEventSystem __Current;
    void OnEnable()
    {

        __Current = this;
        
    }

    static public CallbackEventSystem Current
    {
        get 
        {
            if(__Current == null)
            {
                __Current = GameObject.FindObjectOfType<CallbackEventSystem>();
            }

            return __Current;
        }
    }

    delegate void EventListener(EventInfo ei);

    Dictionary<System.Type, List<EventListener>> eventListeners;

    public void RegisterListener<T>(System.Action<T> listener) where T:EventInfo
    {
        System.Type eventType = typeof(T);

        if(eventListeners == null)
        {
            eventListeners = new Dictionary<System.Type, List<EventListener>>();

        }

        if(eventListeners.ContainsKey(eventType)==false || eventListeners[eventType] == null)
        {
            eventListeners[eventType] = new List<EventListener>();
        }

        // Wrap a type converstion around the event listener
        // to ensure the event info parameter that the listener
        // function takes in is of type T
        EventListener wrapper = (eventInfo) => {listener((T) eventInfo);};

        eventListeners[eventType].Add(wrapper);

    } 
    public void UnregisterListener<T>(System.Action<T> listener) where T : EventInfo
    {
            System.Type eventType = typeof(T);

            if(eventListeners == null || eventListeners.ContainsKey(eventType)==false || eventListeners[eventType] == null)
            {
                return;
            }

            EventListener wrapper = (eventInfo) => {listener((T) eventInfo);};

            eventListeners[eventType].Remove(wrapper);
    }

    public void FireEvent(EventInfo eventInfo)
    {
        System.Type EventInfoSubclassType = eventInfo.GetType();

        if(eventListeners == null || eventListeners[EventInfoSubclassType] == null)
        {
            //There is no eventInfo listeners that are registered 
            //for this eventInfo, so we can't fire an event

            return;
        }

        foreach(EventListener eventListener in eventListeners[EventInfoSubclassType])
        {
            eventListener(eventInfo);
        }
    }

}
