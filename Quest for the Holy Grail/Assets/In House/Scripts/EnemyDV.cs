using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDV : MonoBehaviour
{
    public bool inWindZone = false;
    public GameObject windZone;
    public GameObject[] enemy;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (inWindZone)
        {
            rb.AddForce(transform.forward * windZone.GetComponent<WindArea>().strength);
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "windArea")
        {
            windZone = coll.gameObject;
            inWindZone = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if(coll.gameObject.tag == "windArea")
        {
            inWindZone = false;
        }
    }

}
