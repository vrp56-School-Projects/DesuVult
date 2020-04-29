using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : Stat
{
    void Start()
    {
        EventManager.DeusVultPickup += add;
    }

    void onDisable()
    {
        EventManager.DeusVultPickup -= add;
    }
}
