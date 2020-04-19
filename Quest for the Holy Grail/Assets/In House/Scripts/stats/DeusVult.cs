using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeusVult : Stat
{
    void Start() {
        Pickup.onPlayerCollideDeusVult += add;
    }
}
