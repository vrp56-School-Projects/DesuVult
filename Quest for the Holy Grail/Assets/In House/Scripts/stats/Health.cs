using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Stat
{
    void Start() {
        EventManager.HealthPickup += add;
    }

    void onDisable() {
        EventManager.HealthPickup -= add;
    }
}
