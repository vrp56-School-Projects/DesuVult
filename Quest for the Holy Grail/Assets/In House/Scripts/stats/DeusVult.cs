using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeusVult : Stat
{
    void Start() {
        EventManager.DeusVultPickup += add;
    }

    void onDisable() {
        EventManager.DeusVultPickup -= add;
    }

}
