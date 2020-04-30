using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour
{

    [SerializeField] private Text notifcationText;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(ShowNotification());
    }

    public void SetNotification(string notification)
    {
        notifcationText.text = notification;
        
    }

    IEnumerator ShowNotification()
    {
        GetComponent<FadeObject>().StartFade(0);
        yield return new WaitForSeconds(1.5f);

        yield return new WaitForSeconds(5);
        GetComponent<FadeObject>().StartFade(1);
    }
}
