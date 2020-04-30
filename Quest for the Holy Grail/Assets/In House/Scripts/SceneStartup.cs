using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.EnemyDied += PlayerInfo.IncrementKills;
        EventManager.ChallengeCompleted += PlayerInfo.IncrementPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
