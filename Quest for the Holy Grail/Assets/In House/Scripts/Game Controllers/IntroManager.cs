using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{

    public GameObject light1, light2, light3;
    public float l1_appear, l1_disappear, l2_appear, l2_disappear, l3_appear, l3_disappear;
    public float nextSceneDelay;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AppearRoutine(l1_appear, light1));
        StartCoroutine(DisappearRoutine(l1_disappear, light1));
        StartCoroutine(AppearRoutine(l2_appear, light2));
        StartCoroutine(DisappearRoutine(l2_disappear, light2));
        StartCoroutine(AppearRoutine(l3_appear, light3));
        StartCoroutine(DisappearRoutine(l3_disappear, light3));
        StartCoroutine(EndCutscene(nextSceneDelay));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            NextScene();
        }
    }

    IEnumerator DisappearRoutine(float d, GameObject o)
    {
        yield return new WaitForSeconds(d);
        o.SetActive(false);
    }

    IEnumerator AppearRoutine(float d, GameObject o)
    {

        yield return new WaitForSeconds(d);

        o.SetActive(true);
    }

    IEnumerator EndCutscene(float d)
    {
        yield return new WaitForSeconds(d);

        NextScene();
    }

    public void NextScene()
    {
        /**
            Set proper scene
        */
        SceneManager.LoadScene("SwordSelect");
    }

    public void PlayClip(int clip)
    {
        GetComponent<AudioSource>().clip = clips[clip];
        GetComponent<AudioSource>().Play();
    }
}
