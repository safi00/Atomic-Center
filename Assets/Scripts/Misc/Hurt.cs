using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour, IEvent
{
    /// <summary>
    /// This methods invokes the events that most other scripts are subrcibed to.
    /// </summary>
    public static event Action OnPlayerWallHitCollision;
    public void playEvent(string eventName)
    {
        if (eventName == "HURT")
        {
            Debug.Log(eventName);
            OnPlayerWallHitCollision?.Invoke();
        }
    }
}
