using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour, IEvent
{
    public static event Action OnSpeedGained;
    public void playEvent(string eventName)
    {
        if (eventName == "SPEED")
        {
            Debug.Log(eventName);
            OnSpeedGained?.Invoke();
        }
    }
}
