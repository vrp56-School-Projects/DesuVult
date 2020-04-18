using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestSamuraiController : MonoBehaviour
{
    private Transform _player;
    private NavMeshAgent _navMeshAgent;
    private SamuraiAttackSlotManager _slotManager;
    private float _distance;
    private float _attackRange = 15.0f;
    private int _attackSlot = -1;

   

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _slotManager = _player.GetComponentInParent<SamuraiAttackSlotManager>();

        _distance = GetDistanceToPlayer();
        //Idle();
    }

    // if the samurai is idle i need to check if the player is within aggro range
    // then i need to check for an open attack slot and reserve it if possible
    // if no open attack slot, fill a wait slot 
    // attack when in range if in attack slot at specified rate (NEED TO FIND ATTACK ANIMS)
    // should also do coroutines for timing 

    private float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, _player.position);
    }

    // Check if player is in aggro range and then attemp to fill and attack slot
    private void Idle()
    {
        Debug.Log("GOT HERE");
    }

    private void Update()
    {
        // Determine distance between player and samurai
        _distance = GetDistanceToPlayer();

        
        // look at the player
        if (_player.GetComponentInParent<playerMovement>().isGrounded)
        {
            transform.LookAt(_player);
        }

       
    }
}
