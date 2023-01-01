using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    /// <summary>
    /// This methods invokes the events that most other scripts are subrcibed to.
    /// </summary>
    private void eventCheck(Collider collider, String eventName)
    {
        IEvent events = collider.GetComponent<IEvent>();
        if (events != null)
        {
            events.playEvent(eventName);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ICollectable col = other.GetComponent<ICollectable>();
        if (col != null)
        {
            if (other.CompareTag("Coin"))
            {
                col.Collect("COIN");
            }
            if (other.CompareTag("QCoin"))
            {
                col.Collect("QCOIN");
            }
        }
        if (other.CompareTag("Snake"))
        {
            eventCheck(other, "BITE");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            eventCheck(collision.collider, "HURT");
        }
    }
}
