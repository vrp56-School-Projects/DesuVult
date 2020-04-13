using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SamuraiController : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private float _attackRate = 1.5f;
    
    private Transform _player;
    private NavMeshAgent _navMeshAgent;
    private SamuraiAttackSlotManager _slotManager;
    private int _slot = -1;
    private float _pathtime = 0f;
    private float _attackTime = 0f;
    private float _attackRange = 15.0f;
    private float _attackDistance = 0f;
    private float _distance;

   



    // Start is called before the first frame update
    void Start()
    {
        // Set variables for the AI to use
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _slotManager = _player.GetComponentInParent<SamuraiAttackSlotManager>();
        _attackDistance = _slotManager.distance + 0.5f;
    }



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
            else if((_distance > _attackRange) && (_slot != -1))
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
            Debug.Log(gameObject.name + " Attacked player");
        }

        if (_player.GetComponentInParent<playerMovement>().isGrounded)
        {
            transform.LookAt(_target.transform);
        }

        

    }
}
