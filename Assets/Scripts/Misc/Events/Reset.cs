using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour, IEvent
{
    /// <summary>
    /// This methods invokes the events that most other scripts are subrcibed to.
    /// </summary>
    public static event Action OnReset;
    public void playEvent(string eventName)
    {
        if (eventName == "Reset")
        {
            Debug.Log(eventName);
            OnReset?.Invoke();
        }
    }
}