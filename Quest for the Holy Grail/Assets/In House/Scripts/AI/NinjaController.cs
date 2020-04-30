using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NinjaController : MonoBehaviour
{
    [SerializeField] private float _attackRadius = 20.0f;
    private float _distance = 0f;
    private float _patrolTimer = 0f;
    private float _patrolDelay = 1.5f;
    private int _slot = -1;
    private float _attackTimer = 0f;
    private float _attackDelay = 2.0f;


    public bool attacking = false;

    Transform target;
    NavMeshAgent agent;
    NinjaAttackSlotManager attackSlotManager;
    NinjaWeaponController weaponController;



    // Start is called before the first frame update
    void Start()
    {
        // Set variables for the AI to use
        // FindPlayer();
        agent = GetComponent<NavMeshAgent>();
        weaponController = GetComponentInChildren<NinjaWeaponController>();



        Patrol();

    }


    private void Patrol()
    {
        _patrolTimer = 0f;
        GetRandomLocation(10f);

    }

    private void GetRandomLocation(float range)
    {
        float currentX = gameObject.transform.position.x;
        float currentZ = gameObject.transform.position.z;

        float newX = Random.Range(-range, range) + currentX;
        float newZ = Random.Range(-range, range) + currentZ;

        Vector3 randomLocation = new Vector3(newX, gameObject.transform.position.y, newZ);

        agent.SetDestination(randomLocation);
        Debug.Log("Location set");

    }

    private void GetAttackSlot()
    {
        Debug.Log("Fill attack slot");

        if (attackSlotManager != null)
        {
            if (_slot == -1)
                _slot = attackSlotManager.Reserve(gameObject);
            if (_slot == -1)
                return;
            if (agent == null)
                return;
            agent.Warp(attackSlotManager.GetSlotPosition(_slot));

            Debug.Log(gameObject.name + " Teleports behind you");
            attacking = true;
            //Attack();

        }
    }

    private void Attack()
    {
        Debug.Log("Object thrown");
        // face player and throw
        transform.LookAt(target.position);
        weaponController.ThrowWeapon();
        _attackTimer = 0f;

    }

    private void FindPlayer()
    {
        target = PlayerManager.instance.player.transform;
        attackSlotManager = target.GetComponentInParent<NinjaAttackSlotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // get distance to player
        if (target == null)
            FindPlayer();

        _distance = Vector3.Distance(target.position, transform.position);

        // teleport if player is in attack range
        if (_distance <= _attackRadius)
        {
            if (!attacking)
                GetAttackSlot();

            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _attackDelay && attacking)
                Attack();


            if (target.GetComponentInParent<PlayerController_Sword>().isGrounded)
            {
                // face the player
                transform.LookAt(target.position);
            }

        }
        else
        {
            if (_slot != -1)
            {
                attackSlotManager.Release(_slot);
                attacking = false;
                _slot = -1;
            }
            // _patrolTimer += Time.deltaTime;
            if (_patrolTimer >= _patrolDelay)
                Patrol();
        }

        if (agent.remainingDistance <= 0f)
        {
            _patrolTimer += Time.deltaTime;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}