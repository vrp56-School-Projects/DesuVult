using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private AudioSource swoosh;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void playSound()
    {
        swoosh.PlayOneShot(swoosh.clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
