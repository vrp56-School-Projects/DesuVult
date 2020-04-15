using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkFade : MonoBehaviour
{

    public float FadeDuration, StayDuration;

    bool doBlink;

    void StartBlinking()
    {
        doBlink = true;
        StartCoroutine(Blink());
    }

    void StopBLinking()
    {
        doBlink = false;
    }

    IEnumerator Blink()
    {

        while(true)
        {
            // fade in
            float start = Time.time;
            while(doBlink)
            {
                float percent = (Time.time - start)/FadeDuration;

                GetComponent<CanvasGroup>().alpha = percent;

                if(percent >= 1)
                {    
                    break;
                }
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(StayDuration);

            // fade out
            start = Time.time;
            while(true)
            {
                float percent = (Time.time - start)/FadeDuration;

                GetComponent<CanvasGroup>().alpha = 1-percent;

                if(percent >= 1)
                {    
                    yield return new WaitForSeconds(StayDuration);
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
