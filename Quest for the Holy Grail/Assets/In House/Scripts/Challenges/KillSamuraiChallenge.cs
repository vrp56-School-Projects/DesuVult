using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSamuraiChallenge : Challenge
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.EnemySamuraiDied += IncreaseCount;
    }
}
