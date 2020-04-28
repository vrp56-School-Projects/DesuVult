using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class TestSamuraiController : MonoBehaviour
{
    private float aggroRadius = 15.0f;
    private float _attackDistance = 0f;
    private float _patrolTimer = 0f;
    private float _patrolDelay = 3.0f;
    private float _patrolSpeed = 1.5f;
    public int _attackSlot = -1;
    public int _waitSlot = -1;
    public bool _attacking = false;
    public bool _waiting = false;
    private bool _patroling = false;

    public bool isLookedAt;
    
    Transform target;
    NavMeshAgent agent;
    PlayerController playerControllerScript;
    SamuraiAttackSlotManager attackSlotManager;
    WaitSlotManager waitSlotManager;
    Animator anim;
    WeaponController sword;

    // remove after changing stats code
    public Health playerHealthScript;
    public int health = 3;

    public float _attackRate = 1.34f;
    private float _attackTime = 0f;
    private float _attackDelay = 1.3f; // set to some value when adding animations

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        playerControllerScript = target.GetComponentInParent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        anim = this.GetComponentInParent<Animator>();
        sword = this.GetComponentInChildren<WeaponController>();
        attackSlotManager = target.GetComponentInParent<SamuraiAttackSlotManager>();
        waitSlotManager = target.GetComponentInParent<WaitSlotManager>();
        playerHealthScript = target.GetComponentInChildren<Health>();
        _attackDistance = attackSlotManager.distance + 0.5f;

        SetRandomLocation();
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
                _patroling = false;
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
            _patroling = false;
        }
    }

    private void Attack()
    {
        StartCoroutine(DoDamage(playerHealthScript, _attackDelay));
        Debug.Log(gameObject.name + " Attacked Player");
        
    }

    public IEnumerator DoDamage(Health playerHealth, float delay)
    {
        _attackTime = 0;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(delay);
        

        sword.playSound();
        playerHealth.damage(5f);
    }

    private void SetRandomLocation()
    {
        float currentX = gameObject.transform.position.x;
        float currentZ = gameObject.transform.position.z;

        float xPos = Random.Range(-aggroRadius, aggroRadius) + currentX;
        float zPos = Random.Range(-aggroRadius, aggroRadius) + currentZ;
        
        Vector3 randomLocation = new Vector3(xPos, gameObject.transform.position.y, zPos);

        agent.SetDestination(randomLocation);

        _patrolTimer = 0f;

    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        // Player within aggro range and need to try to fill attack or wait slot
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

                while (_attackTime >= _attackRate)
                {
                    Attack();
                }

                // call function in playercontroller to see if looking at samurai
                isLookedAt = playerControllerScript.IsLooking(transform);
            }
            else isLookedAt = false;

            // Clear wait slot when enemy moves to newly open attack slot
            if (_attacking && _waiting)
            {
                waitSlotManager.Release(_waitSlot);
                _waiting = false;
                _waitSlot = -1;
            }
        }
        // Player not in aggro range and need to go back to patroling
        else
        {
            if (_waitSlot != -1)
            {
                waitSlotManager.Release(_waitSlot);
                _waiting = false;
                _waitSlot = -1;
            }
            if (_attackSlot != -1)
            {
                attackSlotManager.Release(_attackSlot);
                _attacking = false;
                _attackSlot = -1;
            }
            
            if (_patrolTimer >= _patrolDelay)
            {
                SetRandomLocation();
            }
            
        }

        // Start run animation when moving
        if (agent.remainingDistance > 0f)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
            _patrolTimer += Time.deltaTime;
        }

 

    }

   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}
