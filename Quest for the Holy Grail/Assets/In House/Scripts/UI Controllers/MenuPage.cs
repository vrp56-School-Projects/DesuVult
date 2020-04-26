using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPage : MonoBehaviour
{
    void OnEnable()
    {
        // update options in scene
        // Volume
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = Options.GetVolume();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("IntroCutscene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
