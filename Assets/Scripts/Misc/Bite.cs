using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour, IEvent
{
    /// <summary>
    /// This methods invokes the events that most other scripts are subrcibed to.
    /// </summary>
    public static event Action OnBitePain;
    public void playEvent(string eventName)
    {
        if (eventName == "BITE")
        {
            OnBitePain?.Invoke();
        }
    }
}
