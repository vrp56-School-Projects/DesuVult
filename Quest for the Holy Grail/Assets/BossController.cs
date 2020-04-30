using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour, IOnHit
{

    // boss states
    enum State
    {
        MacGuffin,
        Vulnerable,
        Teleport
    }

    [SerializeField] State currentState; // current state of boss
    [SerializeField] int currentCycle; // current cycle (number of times the boss has finished a sequence)
    [SerializeField] Health health;

    // locations
    [SerializeField] Transform[] macGufinLocations;
    [SerializeField] Transform[] teleportLocations;

    Animator anim;
    public GameObject[] MacGuffins;


    // boolean to set when macguffin is killed
    [SerializeField] bool macGuffinKilled;
    [SerializeField] bool bossKilled;

    // Start is called before the first frame update
    void Start()
    {
        // initialize
        anim = GetComponent<Animator>();
        EventManager.StaggerBoss += () => macGuffinKilled = true;
        

        // start in macguffin state
        currentCycle = 0;
        currentState = State.MacGuffin;
        macGuffinKilled = false;
        bossKilled = false;
        
        ExecuteState();
    }

    // triggers next state
    void ExecuteState()
    {
        // start propper coroutine based on current state
        switch(currentState)
        {
            case State.MacGuffin:
                StartCoroutine(ExecuteMacGuffin());
                break;

            case State.Vulnerable:
                StartCoroutine(ExecuteVulnerable());
                break;

            case State Teleport:
                StartCoroutine(ExecuteTeleport());
                break;
        }
    }
    public void OnHit(float damage)
    {
        if (currentState == State.Vulnerable)
        {
            health.damage(damage);
            print("Damaged Boss");
        }
        else
        {
            print("Boss Not Staggered");
        }
    }

    // handles macguffin sequence
    IEnumerator ExecuteMacGuffin()
    {
        // increment cycle number
        currentCycle++;

        // teleport to propper location
        transform.position = teleportLocations[currentCycle-1].position;
        transform.rotation = teleportLocations[currentCycle-1].rotation;

        /*
        =======
        SPAWN MACGUFFIN @ macGuffinLocations[currentCycle-1].position;
        
        ======
        */


        // trigger animation
        anim.SetInteger("state", 0);

        // infinite loop until macguffin killed
        while(!macGuffinKilled) yield return new WaitForEndOfFrame();

        // reset macGuffin Killed
        macGuffinKilled = false;

        // update state
        currentState = State.Vulnerable;

        // start next state
        ExecuteState();
    }

    // handles vulnerable sequence
    IEnumerator ExecuteVulnerable()
    {
        // trigger vulnerable animation
        anim.SetInteger("state", 1);

        /*
        =======
        MAKE DAMAGEABLE
        ======
        */
        health.value = 100;

        // infinite loop until killed
        while (!bossKilled)
        {
            if (health.value == 0) break;
            yield return new WaitForEndOfFrame();
        }


        /*
        =======
        MAKE INVINCEABLE
        ======
        */
        

        // check if game is over
        if(currentCycle == 4)
        {
            // trigger death animation
            anim.SetBool("isDead", true);

            // wait for death animation
            yield return new WaitForSeconds(5);

            // load cutscene
            SceneManager.LoadScene("OutroCutscene2");
        }

        // if not, teleport
        else
        {
            // reset boss killed
            bossKilled = false;

            // update state
            currentState = State.Teleport;

            // start next state
            ExecuteState();
        }
    }

     // handles teleport
    IEnumerator ExecuteTeleport()
    {
        // trigger teleport animation
        anim.SetInteger("state", 2);

        // wait for animation
        yield return new WaitForSeconds(2.5f);

        // update state
        currentState = State.MacGuffin;

        // start next state
        ExecuteState();
    }

}
