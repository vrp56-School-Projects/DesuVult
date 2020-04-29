using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private float damage = 10f;
    void Start()
    {
        direction = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        other.gameObject.GetComponent<IOnHit>()?.OnHit(damage);

    }
}