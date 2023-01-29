using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUP : MonoBehaviour, IEvent
{
    /// <summary>
    /// This methods invokes the events that most other scripts are subrcibed to.
    /// </summary>
    public static event Action OnPointUP;
    public void playEvent(string eventName)
    {
        if (eventName == "PointUP")
        {
            Debug.Log(eventName);
            OnPointUP?.Invoke();
        }
    }
}
