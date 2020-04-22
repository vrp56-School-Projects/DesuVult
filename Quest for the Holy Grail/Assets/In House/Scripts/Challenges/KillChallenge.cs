using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillChallenge : Challenge
{
    [SerializeField] private int count = 0;
    [SerializeField] private int required = 5;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.EnemyDied += UpdateCount;
    }

    // Update is called once per frame
    void UpdateCount()
    {
        count++;
        if (count == required) {
            Complete("Killed 5 enemies!");
        }
    }
}
