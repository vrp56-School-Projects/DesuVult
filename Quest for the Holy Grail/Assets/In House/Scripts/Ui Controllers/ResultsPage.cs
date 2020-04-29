using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsPage : MonoBehaviour
{
    public BlinkFade text;

    public Slider HealthBar, StaminaBar, ManaBar;

    public Text HealthText, StaminaText, ManaText, PointsText,
        KillsText, QuestsText, AbilitiesText;

    float ResetMaxHealth, ResetMaxStamina, ResetMaxMana,
        ResetCurrentHealth, ResetCurrentStamina, ResetCurrentMana;

    int ResetPoints;

    // Start is called before the first frame update
    void Start()
    {
        ResetMaxHealth = PlayerInfo.GetMaxHealth();
        ResetMaxStamina = PlayerInfo.GetMaxStamina();
        ResetMaxMana = PlayerInfo.GetMaxMana();
        ResetCurrentHealth = PlayerInfo.GetCurrentHealth();
        ResetCurrentStamina = PlayerInfo.GetCurrentStamina();
        ResetCurrentMana = PlayerInfo.GetCurrentMana();
        ResetPoints = PlayerInfo.GetPoints();

        UpdateHealth();
        UpdateStamina();
        UpdateMana();
        UpdatePoints();

        UpdatePane2();
    }

    // upgrade health is points != 0
    public void UpgradeHealth()
    {
        if (PlayerInfo.GetPoints() > 0)
        {
            PlayerInfo.SetPoints(PlayerInfo.GetPoints() - 1);
            UpdatePoints();

            PlayerInfo.UpgradeHealth();
            UpdateHealth();
        }
    }

    // upgrade stamina is points != 0
    public void UpgradeStamina()
    {
        if (PlayerInfo.GetPoints() > 0)
        {
            PlayerInfo.SetPoints(PlayerInfo.GetPoints() - 1);
            UpdatePoints();

            PlayerInfo.UpgradeStamina();
            UpdateStamina();
        }
    }

    // upgrade mana is points != 0
    public void UpgradeMana()
    {
        if (PlayerInfo.GetPoints() > 0)
        {
            PlayerInfo.SetPoints(PlayerInfo.GetPoints() - 1);
            UpdatePoints();

            PlayerInfo.UpgradeMana();
            UpdateMana();
        }
    }

    // upgrade health is points != 0 and stats aren't maxed out
    public void Heal()
    {
        if (PlayerInfo.GetPoints() > 0 && (
            PlayerInfo.GetMaxHealth() != PlayerInfo.GetCurrentHealth() ||
            PlayerInfo.GetMaxStamina() != PlayerInfo.GetCurrentStamina() ||
            PlayerInfo.GetMaxMana() != PlayerInfo.GetCurrentMana()
        ))
        {
            PlayerInfo.SetPoints(PlayerInfo.GetPoints() - 1);
            PlayerInfo.Heal();

            UpdateHealth();
            UpdateStamina();
            UpdateMana();
            UpdatePoints();
        }
    }

    // reset initial allocations
    public void Reset()
    {
        PlayerInfo.SetInfo(ResetMaxHealth, ResetMaxStamina, ResetMaxMana,
            ResetCurrentHealth, ResetCurrentStamina, ResetCurrentMana, ResetPoints);

        UpdateHealth();
        UpdateStamina();
        UpdateMana();
        UpdatePoints();
    }

    // update health from PlayerInfo
    void UpdateHealth()
    {
        HealthBar.maxValue = PlayerInfo.GetMaxHealth();
        HealthBar.value = PlayerInfo.GetCurrentHealth();
        HealthText.text = "Health: " + PlayerInfo.GetCurrentHealth().ToString() +
            "/" + PlayerInfo.GetMaxHealth().ToString();
    }

    // update stamina from PlayerInfo
    void UpdateStamina()
    {
        StaminaBar.maxValue = PlayerInfo.GetMaxStamina();
        StaminaBar.value = PlayerInfo.GetCurrentStamina();
        StaminaText.text = "Stamina: " + PlayerInfo.GetCurrentStamina().ToString() +
            "/" + PlayerInfo.GetMaxStamina().ToString();
    }

    // update mana from PlayerInfo
    void UpdateMana()
    {
        ManaBar.maxValue = PlayerInfo.GetMaxMana();
        ManaBar.value = PlayerInfo.GetCurrentMana();
        ManaText.text = "Mana: " + PlayerInfo.GetCurrentMana().ToString() +
            "/" + PlayerInfo.GetMaxMana().ToString();
    }

    // update points from PlayerInfo
    void UpdatePoints()
    {
        PointsText.text = "Points Left: " + PlayerInfo.GetPoints().ToString();
    }

    // update details in pane2
    void UpdatePane2()
    {
        /*
            TO-DO:

            Update # Kills
            Update # total quests
            Update unlocked abilities
        */
    }
}
