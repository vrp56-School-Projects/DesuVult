using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkFade : MonoBehaviour
{
    public float FadeDuration, StayDuration;

    [SerializeField] bool doBlink = false;

    void Start()
    {
        if (doBlink) StartBlinking();
    }

    public void StartBlinking()
    {
        doBlink = true;
        StartCoroutine(Blink());
    }

    public void StopBlinking()
    {
        doBlink = false;
    }

    IEnumerator Blink()
    {

        while (doBlink)
        {
            // fade in
            float start = Time.time;
            while (true)
            {
                float percent = (Time.time - start) / FadeDuration;

                GetComponent<CanvasGroup>().alpha = percent;

                if (percent >= 1 || doBlink == false)
                {
                    break;
                }
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(StayDuration);

            // fade out
            start = Time.time;
            while (true)
            {
                float percent = (Time.time - start) / FadeDuration;

                GetComponent<CanvasGroup>().alpha = 1 - percent;

                if (percent >= 1 || doBlink == false)
                {
                    yield return new WaitForSeconds(StayDuration);
                    break;
                }
                yield return new WaitForEndOfFrame();
            }

            GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}
