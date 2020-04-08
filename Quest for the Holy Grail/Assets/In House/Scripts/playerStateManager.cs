using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStateManager : MonoBehaviour
{

    public CharacterController controller;
    public Stat health;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health.value == 0) {controller.enabled = false;}
    }
}
