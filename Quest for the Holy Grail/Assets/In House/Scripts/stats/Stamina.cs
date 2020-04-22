using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : Stat
{
    void Start() {
        EventManager.StaminaPickup += add;
    }

    void onDisable() {
        EventManager.StaminaPickup -= add;
    }

}
