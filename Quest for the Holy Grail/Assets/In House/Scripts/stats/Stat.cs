using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public float value = 100f;
    public float min = 0f;
    public float max = 100f;
    public float regenerationRate = 0f;

    public bool limitRange = true;


    void Update() {
        value += regenerationRate * Time.deltaTime;
        clamp();
    }

    public void damage(float amount) {
        subtract(amount);
        clamp();
    }

    public void subtract(float amount) {
        value -= amount;
        clamp();
    }

    public void add(float amount) {
        value += amount;
        clamp();
    }

    void clamp() {
        if (limitRange) {
            value = Mathf.Clamp(value, min, max);
        }
    }

}
