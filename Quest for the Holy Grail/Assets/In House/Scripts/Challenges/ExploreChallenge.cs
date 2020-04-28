using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreChallenge : Challenge
{
    void Start()
    {
        EventManager.Explore += base.IncreaseCount;
    }


}
