using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePage : OptionsPage
{
    public Text ActiveQuestsText, CompletedQuestsText;

    void Awake()
    {
        // ActiveQuestsText.text = Get the active quests
        // CompletedQuestsText.text = Get the number of completed quests
        
    }

    public void Unpause()
    {
        // unpause game
        gameObject.SetActive(false);
    }
}
