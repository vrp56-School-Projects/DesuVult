using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaWeaponController : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    private int _weaponIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ThrowWeapon()
    {
        StartCoroutine(Spawn());

    }

    IEnumerator Spawn()
    {
        _weaponIndex = Random.Range(0, weapons.Length);

        Vector3 location = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        yield return new WaitForSeconds(0.2f);

        GameObject newWeapon = Instantiate(weapons[_weaponIndex], location, gameObject.transform.rotation);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
