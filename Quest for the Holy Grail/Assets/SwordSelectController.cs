using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSelectController : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    string selectedSword = "";

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.tag == "Sword" && hit.collider.name != selectedSword)
            {
                // show outline
                hit.collider.gameObject.GetComponent<OutlineObject>().ShowOutline();

                // hide old outline
                if(selectedSword != "") GameObject.Find(selectedSword).GetComponent<OutlineObject>().HideOutline();
                

                selectedSword = hit.collider.name;
            }
        }
    }
}
