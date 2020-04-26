using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public IEnumerator StartGameRoutine()
    {
        

        yield return new WaitForSeconds(.5f);

        
    }
}
