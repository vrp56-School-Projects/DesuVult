using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public enum FadeType
    {
        FadeIn,
        FadeOut,
        InAndOut
    }

    public float FadeDuration;
    public FadeType type;

    // void Start()
    // {
    //     StartCoroutine();
    // }

    // // Fade in or out
    // IEnumerator FadeRoutine()
    // {
    //     float start = Time.time;
    //     while(true)
    //     {
    //         float percent = (Time.time - start)/FadeDuration;

    //         if(FadeIn) GetComponent<CanvasGroup>().alpha = percent;
    //         else GetComponent<CanvasGroup>().alpha = 1-percent;

    //         if(percent >= 1)
    //         {    
    //             if(!FadeIn) gameObject.SetActive(false);
    //             break;
    //         }
    //         yield return new WaitForEndOfFrame();
    //     }
    // }
}
