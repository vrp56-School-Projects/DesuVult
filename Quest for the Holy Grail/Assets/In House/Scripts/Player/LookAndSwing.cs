using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAndSwing : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float raycastDistance = 3f;

    // Update is called once per frame
    void Update()
    {
        //Look at and swing at
        LookingAt();
    }

    //Raises PlayerLooked event and returns whatever object has been looked at
    RaycastHit LookingAt()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            EventManager.CallPlayerLooked(hit);
            hit.transform.gameObject.GetComponent<IOnLook>()?.OnLook();
            return hit;
        }
        return new RaycastHit();
    }

    //Coroutine for timing sword swing after click
    public void Hit(float damage)
    {
            RaycastHit hit;
            hit = LookingAt();
            //Make sure we have an actual RaycastHit object and not a dummy
            if (hit.transform ? true : false)
            {
                hit.transform.gameObject.GetComponent<IOnHit>()?.OnHit(damage);
                gameObject.GetComponent<Sound>().DoSound();
        }
    }
}
