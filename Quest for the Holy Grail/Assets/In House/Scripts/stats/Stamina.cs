﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : Stat
{
    void Start() {
        Pickup.onPlayerCollideStamina += add;
    }
}
