﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Stat
{
    void Start() {
        Pickup.onPlayerCollideHealth += add;
    }
}
