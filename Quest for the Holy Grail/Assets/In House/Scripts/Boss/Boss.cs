using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    private BossState state = BossState.Invulnerable;
    [SerializeField] private Health health;
    void Start()
    {
        EventManager.EnemyDamaged += OnDamage;
        EventManager.StaggerBoss += OnStaggered;
    }

    void OnDisable() {
        EventManager.EnemyDamaged -= OnDamage;
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

    void OnDamage(float damage, GameObject GO) {
        if (state == BossState.Staggered && GO == gameObject) {
            health.damage(damage);
            print("Damaged Boss");
        }
    }

    void OnStaggered(){
        state = BossState.Staggered;
    }
}
