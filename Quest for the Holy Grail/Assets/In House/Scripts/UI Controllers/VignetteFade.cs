using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VignetteFade : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float damageMaxThreshold = 50;
    [SerializeField] private float fadeSpeed = 10;
    private float damageTotal = 0;
    // Start is called before the first frame update
    void Start()
    {
        setAlpha(0);
        EventManager.PlayerDamaged += onDamaged;
    }

    void OnDisable() {
        EventManager.PlayerDamaged -= onDamaged;
    }

    void Update() {
        setAlpha(damageRatio());
        damageTotal -= fadeSpeed * Time.deltaTime;
        damageTotal = Mathf.Clamp(damageTotal, 0, damageMaxThreshold);
    }


    void onDamaged(float damage)
    {   
        damageTotal += damage;
    }

    void setAlpha(float alpha) {
        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }

    float getAlpha() {
        return image.color.a;
    }

    //gives a ratio between accumulated damage and the threshold
    //clamped between 0 and 1
    float damageRatio() {
        return Mathf.Clamp(damageTotal/damageMaxThreshold,0,1f);
    }

}
