using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAndSwing : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Stamina stamina;
    [SerializeField] private Camera camera;
    [Header("Attacking")]
    [SerializeField] private float swingCost = 10;
    [SerializeField] private float swingSpeed = .2f;
    [SerializeField] private float swordDamage = 10f;
    [SerializeField] private float raycastDistance = 3f;

    private bool swingingSword = false;

    // Update is called once per frame
    void Update()
    {
        //Look at and swing at
        LookingAt();
        if(Input.GetButtonDown("Fire1") && !swingingSword && stamina.value >= swingCost){
            print("swinging");
            swingingSword = true;
            StartCoroutine("swingSword");
            stamina.subtract(swingCost);
        }   
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
    IEnumerator swingSword() {
      RaycastHit hit;
      yield return new WaitForSeconds(swingSpeed);
      print("swung");
      swingingSword = false;
      hit = LookingAt();
      //Make sure we have an actual RaycastHit object and not a dummy
      if (hit.transform? true : false){
        // EventManager.CallEnemyDamaged(10f, hit.transform.gameObject);
        hit.transform.gameObject.GetComponent<IOnHit>()?.OnHit(swordDamage);
      }

    }
}
