using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // static player info variables
    static float MaxHealth = 100f;
    static float MaxStamina = 100f;
    static float MaxMana = 100f;

    static float CurrentHealth = 50f;
    static float CurrentStamina = 50f;
    static float CurrentMana = 50f;

    static int Points = 10;

    static int SelectedSword;
    /*
        SELECTION INDEX:

        0: NATSUKI
        1: SATOMI
        2: MEGUMI
        3: HARUNO
    */

    // getters
    public static float GetMaxHealth()
    {
        return MaxHealth;
    }

    public static float GetMaxStamina()
    {
        return MaxStamina;
    }

    public static float GetMaxMana()
    {
        return MaxMana;
    }

    public static float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public static float GetCurrentStamina()
    {
        return CurrentStamina;
    }

    public static float GetCurrentMana()
    {
        return CurrentMana;
    }

    public static int GetPoints()
    {
        return Points;
    }

    public static int GetSwordIndex()
    {
        return SelectedSword;
    }

    // setters
    public static void UpgradeHealth()
    {
        if(CurrentHealth == MaxHealth) CurrentHealth += 10;
        MaxHealth += 10;
    }

    public static void UpgradeStamina()
    {
        if(CurrentStamina == MaxStamina) CurrentStamina += 10;
        MaxStamina += 10;
    }

    public static void UpgradeMana()
    {
        if(CurrentMana == MaxMana) CurrentMana += 10;
        MaxMana += 10;
    }

    public static void SetPoints(int points)
    {
        Points = points;
    }

    public static void SetSwordIndex(int index)
    {
        SelectedSword = index;
    }

    // set all variables
    public static void SetInfo(float newMaxHealth, float newMaxStamina, float newMaxMana, float newCurrentHealth, float newCurrentStamina, float newCurrentMana, int newPoints)
    {
        MaxHealth = newMaxHealth;
        MaxStamina = newMaxStamina;
        MaxMana = newMaxMana;
        CurrentHealth = newCurrentHealth;
        CurrentStamina = newCurrentStamina;
        CurrentMana = newCurrentMana;
        Points = newPoints;
    }

    // set current to max
    public static void Heal()
    {
        CurrentHealth = MaxHealth;
        CurrentStamina = MaxStamina;
        CurrentMana = MaxMana;
    }
}
