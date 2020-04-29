using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] FadeObject BG;
    [SerializeField] AudioSource musicSource;
    float begin;

    // Start is called before the first frame update
    void Start()
    {
        begin = Time.time + 7f;
        StartCoroutine(DelayedEnd());
        StartCoroutine(Sounds());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time > begin)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            StartCoroutine(End());
        }
    }

    IEnumerator DelayedEnd()
    {
        yield return new WaitForSeconds(55);
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        BG.StartFade(0);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator Sounds()
    {
        yield return new WaitForSeconds(1);
        musicSource.Play();
    }
}
