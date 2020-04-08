using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public int value = 100;
    readonly int[] limits = {0,100};

    public bool limitRange = true;

    public void Damage(int amount) {
        subtract(amount);
        clamp();
    }

    public void subtract(int amount) {
        value -= amount;
        clamp();
    }

    public void add(int amount) {
        value += amount;
        clamp();
    }

    void clamp() {
        if (limitRange) {
            value = Mathf.Clamp(value, limits[0], limits[1]);
        }
    }
}
