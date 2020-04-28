using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{

    enum Scene
    {
        beforeBossBattle,
        afterBossBattle
    };

    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject spaceToContinue;
    [SerializeField] private Scene currentScene;
    [SerializeField] private float textSpeed;
    [SerializeField] private float delay;

    private string[] scene1 = {
        "one",
        "two",
        "three"
    };

    private string[] scene2 = {
        "one",
        "two",
        "three"
    };

    void Start()
    {
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        string[] cutscene = (int) currentScene == 0 ? scene1 : scene2;

        yield return new WaitForSeconds(delay);

        foreach(string s in cutscene)
        {
            // clear box
            dialogueText.text = "";
            spaceToContinue.SetActive(false);

            foreach(char c in s)
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(1/textSpeed);
            }

            spaceToContinue.GetComponent<CanvasGroup>().alpha = 0;
            spaceToContinue.GetComponent<FadeObject>().StartFade(4);

            while(!Input.GetKey(KeyCode.Space)) yield return new WaitForEndOfFrame();
        }
    }
}
