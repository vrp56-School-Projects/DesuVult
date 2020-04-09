using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStateManager : MonoBehaviour
{

    public Transform model;
    public Health health;
    float deathRotation = 0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deathRotation += Mathf.Clamp(deathRotation+1f,0f,90f);
        if (health.value == 0) {model.Rotate(deathRotation,0f,0f);}
    }
}
