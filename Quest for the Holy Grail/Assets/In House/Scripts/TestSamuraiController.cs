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
    public bool isLookedAt;


    Transform target;
    NavMeshAgent agent;
    PlayerController playerControllerScript;
    SamuraiAttackSlotManager attackSlotManager;
    WaitSlotManager waitSlotManager;
    Animator anim;


    // remove after changing stats code
    Health playerHealthScript;
    public float _attackRate = 1.5f;
    private float _attackTime = 0f;
    private float _attackDelay = 0.0f; // set to some value when adding animations

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        playerControllerScript = target.GetComponentInParent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        anim = this.GetComponentInParent<Animator>();
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
        StartCoroutine(DoDamage(playerHealthScript, _attackDelay));
        Debug.Log(gameObject.name + " Attacked Player");
        _attackTime = 0;
    }

    IEnumerator DoDamage(Health playerHealth, float delay)
    {
        yield return new WaitForSeconds(delay);

        playerHealth.damage(5f);
    }


    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        // Start run animation when moving
        if (agent.remainingDistance > 0f)
        {
            anim.SetBool("Run", true);
        }
        else anim.SetBool("Run", false);

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
                _attackTime += Time.deltaTime;

                while (_attackTime > _attackRate)
                {
                    Attack();
                }

                // call function in playercontroller to see if looking at samurai
                isLookedAt = playerControllerScript.IsLooking(transform);
            }

            // Clear wait slot when enemy moves to newly open attack slot
            if (_attacking && _waiting)
            {
                waitSlotManager.Release(_waitSlot);
                _waiting = false;
                _waitSlot = -1;
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
