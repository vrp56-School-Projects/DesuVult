using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NinjaController : MonoBehaviour
{
    private Transform _player;
    private Vector3 _recentPlayerPos;
    private NavMeshAgent _navMeshAgent;
    private int _slot = -1;
    private float _pathtime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        // Set variables for the AI to use
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _recentPlayerPos = _player.position;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        //_navMeshAgent.stoppingDistance = 1f;



    }
  

    // Update is called once per frame
    void Update()
    {
        
        _pathtime += Time.deltaTime;

        if (_pathtime > 0.5f)
        {
            if (_player.position - _recentPlayerPos == new Vector3(0, 0, 0))
            {
                return;
            }
            else
            {
                _pathtime = 0f;
                var slotManager = _player.GetComponent<NinjaAttackSlotManager>();
                if (slotManager != null)
                {
                    if (_slot == -1)
                    {
                        _slot = slotManager.Reserve(gameObject);

                    }
                    if (_slot == -1)
                        return;
                    // Set the destination for the AI (player location)
                    if (_navMeshAgent == null)
                    {
                        return;
                    }
                    //_navMeshAgent.SetDestination(slotManager.GetSlotPosition(_slot));
                    _navMeshAgent.Warp(slotManager.GetSlotPosition(_slot));
                    _recentPlayerPos = _player.position;

                }

                // look at player
                //if (_navMeshAgent.remainingDistance <= 3f)
                //{
                //    this.transform.LookAt(_player);
                //}
            }

        }
    }
}
