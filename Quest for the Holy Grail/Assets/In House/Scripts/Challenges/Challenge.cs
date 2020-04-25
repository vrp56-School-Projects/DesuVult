using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    [SerializeField] private int count = 0;
    [SerializeField] private int needed = 5;
    [SerializeField] private string message = "Message Name";
    protected void Complete(string message) {
        Debug.Log(message);
    }
    protected void IncreaseCount() {
        count++;
        checkStatus();
    }
    protected void SetMessage(string newMessage){
        message = newMessage;
    }

    void checkStatus() {
        if (count == needed) {
            Complete(message);
        }
    }
}
