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
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        
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

            gameObject.GetComponent<TestSamuraiController>().health -= 2;
            rb.AddForce(transform.position * windZone.GetComponent<WindArea>().strength, ForceMode.Acceleration);
            gameObject.GetComponent<TestSamuraiController>()._waiting = false;
            gameObject.GetComponent<TestSamuraiController>()._attacking = false;
            sam.Release(gameObject.GetComponent<TestSamuraiController>()._attackSlot);
            sam.Release(gameObject.GetComponent<TestSamuraiController>()._waitSlot);
            gameObject.GetComponent<TestSamuraiController>()._attackSlot = -1;
            gameObject.GetComponent<TestSamuraiController>()._waitSlot = -1;
            inWindZone = false;

        }

        
    }

    private void OnTriggerExit(Collider coll)
    {
        if(coll.gameObject.tag == "windArea")
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
