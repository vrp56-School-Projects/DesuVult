using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void onCollision(float value);



public class Pickup : MonoBehaviour
{
    private enum statType {
        Health,
        Stamina,
        DeusVult
    }   

    public static event onCollision onPlayerCollideHealth;
    public static event onCollision onPlayerCollideStamina;
    public static event onCollision onPlayerCollideDeusVult;
    public Collider player;
    [SerializeField] private statType stat;
    public float value = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other) {

        switch(stat){
            case statType.Health:
            onPlayerCollideHealth?.Invoke(value);
            break;
            case statType.Stamina:
            onPlayerCollideStamina?.Invoke(value);
            break;
            case statType.DeusVult:
            onPlayerCollideDeusVult?.Invoke(value);
            break;
        }
        // onPlayerCollide(value);
        Destroy(gameObject);
    
    }
}
