using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillNinjaChallenge : Challenge
{
    void Start()
    {
        EventManager.EnemyNinjaDied += IncreaseCount;
    }
}
