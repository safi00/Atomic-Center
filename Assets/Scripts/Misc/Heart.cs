using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, IEvent
{
    /// <summary>
    /// This methods invokes the events that most other scripts are subrcibed to.
    /// </summary>
    public static event Action OnHeartGained;
    public void playEvent(string eventName)
    {
        if (eventName == "HEART")
        {
            Debug.Log(eventName);
            OnHeartGained?.Invoke();
        }
    }
}
