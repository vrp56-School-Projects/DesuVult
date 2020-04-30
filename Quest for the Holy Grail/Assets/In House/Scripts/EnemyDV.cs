using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDV : MonoBehaviour
{
    public bool active = false;
    public bool inWindZone = false;
    public GameObject windZone;
    Rigidbody rb;
    SamuraiAttackSlotManager sam;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        windZone = GameObject.FindGameObjectWithTag("windArea");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (inWindZone)
        {
            rb.AddForce(transform.position * windZone.GetComponent<WindArea>().strength, ForceMode.Acceleration);
            inWindZone = false;

        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "windArea")
        {
            windZone = coll.gameObject;
            inWindZone = true;
        }
        if (inWindZone)
        {

            gameObject.GetComponent<SamuraiController>().GetComponent<Health>().damage(20f);
            rb.AddForce(transform.position * windZone.GetComponent<WindArea>().strength, ForceMode.Acceleration);
            gameObject.GetComponent<SamuraiController>()._waiting = false;
            gameObject.GetComponent<SamuraiController>()._attacking = false;
            sam.Release(gameObject.GetComponent<SamuraiController>()._attackSlot);
            sam.Release(gameObject.GetComponent<SamuraiController>()._waitSlot);
            gameObject.GetComponent<SamuraiController>()._attackSlot = -1;
            gameObject.GetComponent<SamuraiController>()._waitSlot = -1;
            inWindZone = false;

        }


    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "windArea")
        {
            inWindZone = false;
        }
        rb.isKinematic = true;
        MyMethod();
        rb.isKinematic = false;

    }
    public IEnumerator MyMethod()
    {
        yield return new WaitForSeconds(2);
    }
}
