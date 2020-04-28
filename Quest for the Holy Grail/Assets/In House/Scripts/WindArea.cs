using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    public float strength;
    public Vector3 direction;
    public GameObject dv;
    public bool active;
    public Collider dvC;

    public void Start()
    {
    }

    public void Update()
    {
       
    }

    public IEnumerator MyMethod()
    {
        yield return new WaitForSeconds(1);
    }
}
