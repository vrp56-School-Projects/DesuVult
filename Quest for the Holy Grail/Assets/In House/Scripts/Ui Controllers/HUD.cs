using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    [SerializeField] Health health;
    [SerializeField] Stamina stamina;
    [SerializeField] Mana mana;

    public Slider HealthBar, StaminaBar, ManaBar;

    void OnEnable()
    {
        //health = FindObjectOfType<Health>();
        //mana = FindObjectOfType<Mana>();
        //stamina = FindObjectOfType<Stamina>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update Health, Stamina and Mana bars
        HealthBar.value = health.value / health.max;
        StaminaBar.value = stamina.value / stamina.max;
        ManaBar.value = mana.value / mana.max;
    }
}
