using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class triggerTimeline : MonoBehaviour
{
    public UnityEvent customEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            customEvent.Invoke();
        }
    }
}
