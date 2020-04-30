using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPage : MonoBehaviour
{
    public FadeObject BlackScreen;

    void OnEnable()
    {
        // update options in scene
        // Volume
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = Options.GetVolume();
        }
        StartCoroutine(StartMenuRoutine());
    }

    public void StartGame()
    {
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.StartFade(0);
        SceneManager.LoadScene("IntroCutscene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator StartMenuRoutine()
    {
        yield return new WaitForSeconds(4f);
        Button[] buttons = GetComponentsInChildren<Button>();

        foreach (Button b in buttons)
        {
            b.interactable = true;
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.F11))
        {
            SceneManager.LoadScene("OutroCutscene1");
        }
    }
}
