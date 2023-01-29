using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour, IEvent
{
    /// <summary>
    /// This methods invokes the events that most other scripts are subrcibed to.
    /// </summary>
    public static event Action OnLog;
    public void playEvent(string eventName)
    {
        if (eventName == "Log")
        {
            OnLog?.Invoke();
        }
    }
}
