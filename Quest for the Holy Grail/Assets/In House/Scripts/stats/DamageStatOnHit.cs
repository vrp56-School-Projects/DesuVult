using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStatOnHit : MonoBehaviour, IOnHit
{
    [SerializeField] private Stat stat;

    public void OnHit(float damage)
    {
        stat.damage(damage);
    }
}