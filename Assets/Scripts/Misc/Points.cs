using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour, IEvent
{
    /// <summary>
    /// This methods invokes the events that most other scripts are subrcibed to.
    /// </summary>
    public static event Action OnPointsGained;
    public void playEvent(string eventName)
    {
        if (eventName == "POINT")
        {
            Debug.Log(eventName);
            OnPointsGained?.Invoke();
        }
    }
}
