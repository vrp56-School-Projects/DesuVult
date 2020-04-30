using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
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

    // locations
    [SerializeField] Transform[] macGufinLocations;
    [SerializeField] Transform[] teleportLocations;

    Animator anim;

    // boolean to set when macguffin is killed
    bool macGuffinKilled = false;
    bool bossKilled = false;

    // Start is called before the first frame update
    void Start()
    {
        // initialize
        anim = GetComponent<Animator>();

        // start in macguffin state
        currentCycle = 0;
        currentState = State.MacGuffin;
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

        // infinite loop until killed
        while(!bossKilled) yield return new WaitForEndOfFrame();

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
