using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestSamuraiController : MonoBehaviour
{
    public float aggroRadius = 15.0f;
    private int _attackSlot = -1;
    private int _waitSlot = -1;
    private bool _attacking = false;
    private bool _waiting = false;
    private float _attackDistance = 0f;
    

    Transform target;
    NavMeshAgent agent;
    SamuraiAttackSlotManager attackSlotManager;
    WaitSlotManager waitSlotManager;


    // remove after changing stats code
    Health playerHealthScript;
    public float attackSpeed = 1.5f;
    private float _attackCooldown = 0f;
    private float _attackDelay = 0.6f;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        attackSlotManager = target.GetComponentInParent<SamuraiAttackSlotManager>();
        waitSlotManager = target.GetComponentInParent<WaitSlotManager>();
        playerHealthScript = target.GetComponentInChildren<Health>();
        _attackDistance = attackSlotManager.distance + 0.5f;
    }

    

    private void CheckAttackSlot()
    {
        if (attackSlotManager != null)
        {
            if (_attackSlot == -1)
            {
                _attackSlot = attackSlotManager.Reserve(gameObject);
            }
            if (_attackSlot == -1)
                CheckWaitSlot();
            else
            {
                // Set the destination for the AI (player location)
                if (agent == null)
                {
                    return;
                }
                agent.SetDestination(attackSlotManager.GetSlotPosition(_attackSlot));
                _attacking = true;
            }
        }
    }

    private void CheckWaitSlot()
    {
        if (waitSlotManager != null)
        {
            if (_waitSlot == -1)
            {
                _waitSlot = waitSlotManager.Reserve(gameObject);
            }
            if (_waitSlot == -1)
                return;
            // Set the destination for the AI (player location)
            if (agent == null)
            {
                return;
            }
            agent.SetDestination(waitSlotManager.GetSlotPosition(_waitSlot));
            _waiting = true;
        }
    }

    private void Attack()
    {
        if (_attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(playerHealthScript, _attackDelay));
            Debug.Log(gameObject.name + " Attacked Player");
            _attackCooldown = 1 / attackSpeed;
        }
    }

    IEnumerator DoDamage(Health playerHealth, float delay)
    {
        yield return new WaitForSeconds(delay);

        playerHealth.damage(5f);
    }

    

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        _attackCooldown -= Time.deltaTime;

        if (distance < aggroRadius)
        {
            CheckAttackSlot();

            if (target.GetComponentInParent<playerMovement>().isGrounded)
            {
                // face the player
                transform.LookAt(target.position);
            }

            // attack the player if in attack slot
            if (distance <= _attackDistance)
            {
                while (_attacking)
                    Attack();
            }
        }
        else
        {
            if (_waitSlot != -1)
            {
                waitSlotManager.Release(_waitSlot);
                _waiting = false;
                _waitSlot = -1;
                if (agent == null)
                {
                    return;
                }
                agent.ResetPath();
            }
            if (_attackSlot != -1)
            {
                attackSlotManager.Release(_attackSlot);
                _attacking = false;
                _attackSlot = -1;
                // Reset the destination for the AI (Not following)
                if (agent == null)
                {
                    return;
                }
                agent.ResetPath();
            }
            
        }
        
    }

   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}
