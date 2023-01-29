using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour, IEvent
{
    /// <summary>
    /// This methods invokes the events that most other scripts are subrcibed to.
    /// </summary>
    public static event Action OnTurnEnd;
    public void playEvent(string eventName)
    {
        if (eventName == "EndTurn")
        {
            Debug.Log(eventName);
            OnTurnEnd?.Invoke();
        }
    }
}
