using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLoseManager : MonoBehaviour
{

    public void GiveUp()
    {
        ResetStats();
        SceneManager.LoadScene("MainMenu");
    }

    public void TryAgain()
    {
        ResetStats();
        SceneManager.LoadScene("IntroCutscene");
    }

    void ResetStats()
    {
        PlayerInfo.SetInfo(100, 100, 100, 100, 100, 100, 0);
    }
}
