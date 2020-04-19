using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public delegate void eventHandler();
    public static event eventHandler eventHandled;

    protected virtual void onEventHandled() {
        Debug.Log("Event Handled");
    }

    void Start()
    {
        eventHandled += onEventHandled;
    }

    // Update is called once per frame
    void Update()
    {
        if (eventHandled != null) {
            eventHandled();
        }
    }
}
