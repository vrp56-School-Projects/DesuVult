using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillChallenge : Challenge
{
    void Start()
    {
        EventManager.EnemyDied += IncreaseCount;
    }

}
