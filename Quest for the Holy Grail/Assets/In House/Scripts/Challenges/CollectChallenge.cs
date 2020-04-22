using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectChallenge : Challenge
{
    [SerializeField] private int pickupsCollected = 0;
    [SerializeField] private int pickupsNeeded = 3;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Pickup += updatePickupTotal;
    }

    void updatePickupTotal(float value) {
        pickupsCollected++;
        if (pickupsCollected == pickupsNeeded) {
            Complete("Collected 3 Pickups!");
        }
    }
}
