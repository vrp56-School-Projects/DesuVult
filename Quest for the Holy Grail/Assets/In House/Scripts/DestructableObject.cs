using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour, IOnHit
{
    public void OnHit(float damage)
    {
        Destroy(gameObject);
    }
}
