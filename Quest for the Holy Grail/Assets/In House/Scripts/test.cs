using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Debug.Log("Fire1");
            EventManager.CallEnemyDied();
            EventManager.CallEnemySamuraiDied();
            EventManager.CallEnemyNinjaDied();
            EventManager.CallExplored();
            EventManager.CallChallengeCompleted();


        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            EventManager.CallPlayerDamaged(10f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            EventManager.CallPlayerDamaged(20f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            EventManager.CallPlayerDamaged(30f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            EventManager.CallPlayerDamaged(40f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            EventManager.CallPlayerDamaged(50f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            EventManager.CallPlayerDamaged(60f);
        }
    }
}
