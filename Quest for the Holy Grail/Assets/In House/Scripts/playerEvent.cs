using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerEvent : MonoBehaviour
{
    //Let everyone know that the player is dead
    void onDisable()
    {
        EventManager.CallPlayerDied();
    }
}
