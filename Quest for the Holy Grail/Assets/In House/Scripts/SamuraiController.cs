using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SamuraiController : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private float _attackRate = 2f;

    
    private Transform _player;
    private NavMeshAgent _navMeshAgent;
    private SamuraiAttackSlotManager _slotManager;
    private Health _playerHealthScript;
    private Animator _anim;
    private int _slot = -1;
    private float _pathtime = 0f;
    private float _attackTime = 0f;
    private float _attackRange = 15.0f;
    private float _attackDistance = 0f;
    private float _distance;

    // The possible state that a samurai can be in
    private enum _samuraiState
    {
        IDLE,
        ATTACK_SLOT,
        WAIT_SLOT
    }

    // track the current state of the samurai
    private _samuraiState _currentState = _samuraiState.IDLE;


    // Start is called before the first frame update
    void Start()
    {
        // Set variables for the AI to use
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealthScript = _player.GetComponentInChildren<Health>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _slotManager = _player.GetComponentInParent<SamuraiAttackSlotManager>();
        _anim = this.GetComponentInParent<Animator>();
        _attackDistance = _slotManager.distance + 0.5f;
    }

    // if the samurai is idle i need to check if the player is within aggro range
    // then i need to check for an open attack slot and reserve it if possible
    // if no open attack slot, fill a wait slot 
    // attack when in range if in attack slot at specified rate (NEED TO FIND ATTACK ANIMS)
    // should also do coroutines for timing 

    //private void IdleState()
    //{
    //    if (_distance <= _attackRange)
    //    {
    //        // try to fill attack slot 
    //        // if attack slot open, set destination and set state to attackSlot
    //        // if attack slot not open, set state to waitSlot and go set destintaion to a wait slot

    //        FillAttackSlot();
    //        // if (_slot == -1) FillWaitSlot();
    //    }
    //}

    //private void FillAttackSlot()
    //{
    //    if (_slotManager != null)
    //    {
    //        if (_slot == -1)
    //        {
    //            _slot = _slotManager.Reserve(gameObject);
    //        }
    //        if (_slot == -1)
    //        {
    //            _currentState = _samuraiState.WAIT_SLOT;
    //            return;
    //        }

    //        // Set the destination for the AI (player location)
    //        if (_navMeshAgent == null)
    //        {
    //            return;
    //        }
    //        _navMeshAgent.SetDestination(_slotManager.GetSlotPosition(_slot));
    //        _currentState = _samuraiState.ATTACK_SLOT;
    //    }
    //}

    //private void AttackState()
    //{
    //    // check if out of aggro range and release slot if so otherwise
    //    // run to player if not at slot yet
    //    // if at slot, attack at specified rate

    //    if (_distance > _attackRange)
    //    {
    //        _slotManager.Release(_slot);
    //        _slot = -1;
    //        // Reset the destination for the AI (Not following)
    //        if (_navMeshAgent == null)
    //        {
    //            return;
    //        }
    //        _navMeshAgent.ResetPath();
    //        _currentState = _samuraiState.IDLE;
    //    }
    //    // Set the destination for the AI (player location)
    //    if (_navMeshAgent == null)
    //    {
    //        return;
    //    }
    //    // run toward the player
    //    if (_navMeshAgent.remainingDistance > 0f)
    //    {
    //        _navMeshAgent.SetDestination(_slotManager.GetSlotPosition(_slot));
    //        _anim.SetBool("Run", true);
    //    }
    //    else _anim.SetBool("Run", false);

    //    // attack the player
    //    if (_distance <= _attackDistance && _attackTime > _attackRate)
    //    {
    //        _attackTime = 0f;
    //        _playerHealthScript.damage(5f);
    //        // Debug.Log(gameObject.name + " Attacked player");
    //    }
    //}

    //private void FillWaitSlot()
    //{
    //    // for now go into idle state
    //    _currentState = _samuraiState.IDLE;
    //}

    //private void WaitState()
    //{
    //    // go into idle state for now. Shouldn't even get here but redundancy is good
    //    _currentState = _samuraiState.IDLE;

    //    // continue checking if an attack slot opens up and try to fill when open
    //}

    //private void Update()
    //{
    //    Debug.Log(gameObject.name + " Current State: " + _currentState);

    //    // Determine distance between player and samurai
    //    _distance = Vector3.Distance(transform.position, _player.position);

    //    // update attack time
    //    _attackTime += Time.deltaTime;

    //    // look at the player
    //    if (_player.GetComponentInParent<playerMovement>().isGrounded)
    //    {
    //        transform.LookAt(_target.transform);
    //    }

    //    switch (_currentState)
    //    {
    //        case _samuraiState.IDLE:
    //            IdleState();
    //            break;
    //        case _samuraiState.ATTACK_SLOT:
    //            AttackState();
    //            break;
    //        case _samuraiState.WAIT_SLOT:
    //            WaitState();
    //            break;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        // Determine if player is within aggro range
        _distance = Vector3.Distance(transform.position, _player.position);

        _attackTime += Time.deltaTime;
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

        // Attack the Player if in attack slot
        if (_navMeshAgent == null)
        {
            return;
        }
        else if (_distance <= _attackDistance && _attackTime > _attackRate)
        {
            _attackTime = 0f;
            _playerHealthScript.damage(5f);
            Debug.Log(gameObject.name + " Attacked player");
        }

        if (_player.GetComponentInParent<playerMovement>().isGrounded)
        {
            transform.LookAt(_target.transform);
        }

        if (_navMeshAgent.remainingDistance > 0f)
        {
            _anim.SetBool("Run", true);
        }
        else _anim.SetBool("Run", false);
    }
}
