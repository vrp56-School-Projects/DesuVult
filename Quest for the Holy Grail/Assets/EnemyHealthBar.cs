using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Slider>().value == 0)
        {
            GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
