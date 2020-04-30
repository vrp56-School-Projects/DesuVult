using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void onCollision(float value);



public class Pickup : MonoBehaviour
{
    private enum statType
    {
        Health,
        Stamina,
        DeusVult
    }

    public Collider player;
    [SerializeField] private statType stat;
    public float value = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (stat)
            {
                case statType.Health:
                    EventManager.CallHealthPickup(value);
                    break;
                case statType.Stamina:
                    EventManager.CallStaminaPickup(value);
                    break;
                case statType.DeusVult:
                    EventManager.CallDeusVultPickup(value);
                    break;
            }
            EventManager.CallPickup(value);
            Destroy(gameObject);
        }

    }
}
