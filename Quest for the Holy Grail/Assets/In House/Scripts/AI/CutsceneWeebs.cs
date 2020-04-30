using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CutsceneWeebs : MonoBehaviour
{
    private float _patrolTimer = 0f;
    private float _patrolDelay = 1.5f;
    NavMeshAgent agent;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = this.GetComponentInParent<Animator>();
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
        //Debug.Log("Location set");

    }

    // Update is called once per frame
    void Update()
    {
        if (_patrolTimer >= _patrolDelay)
        {
            Patrol();
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
}
