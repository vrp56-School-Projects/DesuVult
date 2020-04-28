using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IOnHit
{
    // Start is called before the first frame update
    private BossState state = BossState.Invulnerable;
    [SerializeField] private Health health;
    void Start()
    {
        EventManager.StaggerBoss += OnStaggered;
    }

    void OnDisable() {
        EventManager.StaggerBoss -= OnStaggered;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    enum BossState {
        Hidden,
        Invulnerable,
        Staggered,

    }

    public void OnHit(float damage) {
        if(state == BossState.Staggered){
            health.damage(damage);
            print("Damaged Boss");
        }
        else {
            print("Boss Not Staggered");
        }
    }

    void OnStaggered(){
        state = BossState.Staggered;
    }
}
