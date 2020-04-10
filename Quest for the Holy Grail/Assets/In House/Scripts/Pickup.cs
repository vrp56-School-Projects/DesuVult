using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Collider player;
    public Stat playerStat;
    public float value = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other) {
        if (other == player) {
            playerStat.add(value);
            Destroy(gameObject);
        }
    }
}
