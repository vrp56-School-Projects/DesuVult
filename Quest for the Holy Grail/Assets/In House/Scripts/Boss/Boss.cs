using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    private BossState state = BossState.Staggered;
    [SerializeField] private Health health;
    void Start()
    {
        EventManager.EnemyDamaged += onDamage;
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

    void onDamage(float damage, GameObject GO) {
        if (state == BossState.Staggered && GO == gameObject) {
            health.damage(damage);
            print("Damaged Boss");
        }
    }
}
