using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTest : MonoBehaviour
{
    public float Strength;
    public Vector3 WindDirection;

    void OnTriggerStay(Collider col)
    {
        Rigidbody colRigidbody = col.GetComponent<Rigidbody>();
        if (colRigidbody != null)
        {
            colRigidbody.AddForce(transform.position * Strength);
        }
    }
}