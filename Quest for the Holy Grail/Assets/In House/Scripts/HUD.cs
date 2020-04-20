using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Health health;
    public Stamina stamina;
    public Mana mana;

    public Slider HealthBar, StaminaBar, ManaBar;

    // Update is called once per frame
    void Update()
    {
        // Update Health, Stamina and Mana bars
        HealthBar.value = health.value/health.max;
        StaminaBar.value = stamina.value/stamina.max;
        ManaBar.value = mana.value/mana.max;
    }
}
