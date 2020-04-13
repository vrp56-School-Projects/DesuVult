using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    public enum FadeType
    {
        FadeIn,
        FadeOut,
        InAndOut
    }

    public float FadeDuration, StayDuration, Delay;
    public FadeType type;

    void Start()
    {
        StartCoroutine(FadeRoutine());
    }

    // Fade in or out
    IEnumerator FadeRoutine()
    {
        // wait for delay
        yield return new WaitForSeconds(Delay);

        // perform first fade
        float start = Time.time;
        while(true)
        {
            float percent = (Time.time - start)/FadeDuration;

            if(type == FadeType.FadeOut) GetComponent<CanvasGroup>().alpha = 1-percent;
            else GetComponent<CanvasGroup>().alpha = percent;

            if(percent >= 1)
            {    
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        // if fade out, set object inactive and end coroutine
        if(type == FadeType.FadeOut)
        {
            gameObject.SetActive(false);
            yield break;
        }

        // if in and out, do out
        if(type == FadeType.InAndOut)
        {

            yield return new WaitForSeconds(StayDuration);

            start = Time.time;
            while(true)
            {
                float percent = (Time.time - start)/FadeDuration;

                GetComponent<CanvasGroup>().alpha = 1-percent;

                if(percent >= 1)
                {    
                    gameObject.SetActive(false);
                    yield break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
