using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour, IEvent
{
    /// <summary>
    /// This methods invokes the events that most other scripts are subrcibed to.
    /// </summary>
    public static event Action OnEventTriggered;
    public void playEvent(string eventName)
    {
        if (eventName == "EventTriggered")
        {
            Debug.Log(eventName);
            OnEventTriggered?.Invoke();
        }
    }
}