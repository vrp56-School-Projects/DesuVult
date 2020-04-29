using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectChallenge : Challenge
{
    // Start is called before the first frame update
    void Start()
    {
        // SetMessage("Collected 3 Pickups!");
        EventManager.Pickup += IncreaseCount;
    }

    //overload base so that it matches our delegate
    void IncreaseCount(float value)
    {
        base.IncreaseCount();
    }
}
