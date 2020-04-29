using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReg : MonoBehaviour, IOnHit
{
    private Health health;
    void Start()
    {
        health = gameObject.GetComponent<Health>();
    }

    public void OnHit(float damage){
        health.damage(damage);
    }
}
