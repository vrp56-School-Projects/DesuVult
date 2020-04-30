using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiWeaponController : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.gameObject.GetComponent<IOnHit>()?.OnHit(damage);
    }
}
