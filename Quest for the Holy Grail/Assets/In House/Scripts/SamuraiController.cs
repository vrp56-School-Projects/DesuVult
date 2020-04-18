using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SamuraiController : MonoBehaviour
{
    private Transform _player;
    private NavMeshAgent _navMeshAgent;
    private SamuraiAttackSlotManager _slotManager;
    private PlayerController _playerControllerScript;
    private Health _playerHealthScript;
    private Animator _anim;
    private int _slot = -1;
    private float _pathtime = 0f;
    private float _attackTime = 1.5f;
    private float _attackRange = 15.0f;
    private float _attackDistance = 0f;
    private float _distance;
    private float _attackRate = 1.5f;
    public bool isLookedAt;
  
    // Start is called before the first frame update
    void Start()
    {
        // Set variables for the AI to use
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealthScript = _player.GetComponentInChildren<Health>();
        _playerControllerScript = _player.GetComponentInParent<PlayerController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _slotManager = _player.GetComponentInParent<SamuraiAttackSlotManager>();
        _anim = this.GetComponentInParent<Animator>();
        _attackDistance = _slotManager.distance + 0.5f;
    }

    // when the samurai is in attack range give the player an option to attack it via button press
    // need to only have it kill the samurai that player is facing
    private void Attack()
    {
        _playerHealthScript.damage(5f);
        Debug.Log(gameObject.name + " Attacked player");
        _attackTime = 0f;
    }

    public void Die()
    {

    }
   

    // Update is called once per frame
    void Update()
    {
        
        // Determine if player is within aggro range
        _distance = Vector3.Distance(transform.position, _player.position);

        
        _pathtime += Time.deltaTime;
        if (_pathtime > 0.5f)
        {
            _pathtime = 0f;
            // Only move attempt to reserve slot if withing aggro range
            if (_distance <= _attackRange)
            {

                if (_slotManager != null)
                {
                    if (_slot == -1)
                    {
                        _slot = _slotManager.Reserve(gameObject);
                    }
                    if (_slot == -1)
                        return;
                    // Set the destination for the AI (player location)
                    if (_navMeshAgent == null)
                    {
                        return;
                    }
                    _navMeshAgent.SetDestination(_slotManager.GetSlotPosition(_slot));
                }
            }
            else if ((_distance > _attackRange) && (_slot != -1))
            {
                _slotManager.Release(_slot);
                _slot = -1;
                // Reset the destination for the AI (Not following)
                if (_navMeshAgent == null)
                {
                    return;
                }
                _navMeshAgent.ResetPath();
            }
        }

        if (_distance <= _attackDistance)  // _anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && 
        {
            _attackTime += Time.deltaTime;
            
            while (_attackTime > _attackRate)
            {
                Attack();
            }

            // call function in playercontroller to see if looking at samurai
            isLookedAt = _playerControllerScript.IsLooking(transform);
        }

        if (_player.GetComponentInParent<playerMovement>().isGrounded)
        {
            transform.LookAt(_player);
        }

        if (_navMeshAgent.remainingDistance > 0f)
        {
            _anim.SetBool("Run", true);
        }
        else _anim.SetBool("Run", false);
    }
}
