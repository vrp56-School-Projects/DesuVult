using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // static player info variables
    static float MaxHealth = 100f;
    static float MaxStamina = 100f;
    static float MaxMana = 100f;

    static float CurrentHealth = 100f;
    static float CurrentStamina = 100f;
    static float CurrentMana = 100f;

    static int TotalQuestsComplete;

    /*
        USE THESE TO FIND WHICH SWORD THE PLAYER IS USING:
        SWORDS CAN BE FOUDN IN PREFABS

        0: NATSUKI
        1: SATOMI
        2: HARUNO
    */
    static int SelectedSword;

    /*
        UPDATE THESE DURING EACH DUNGEON LEVEL
    */
    static int Points = 0;                  // Number of quests completed
    static int TotalEnemiesKilled = 0;      // Number of enemies killed
    static int Abilities = 0;               // Number of Abilities Unlocked

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
