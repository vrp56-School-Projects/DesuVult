using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private bool _lookingAtEnemey = false;
    private GameObject _currentEnemy = null;
    private float _attackCooldown = 0f;
    public float attackSpeed = 2f;
    public GameObject dv;
    public bool active;

    [SerializeField]
    private TextMeshProUGUI _attackText;

    [SerializeField]
    private SpawnManager _spawnManager;

    [SerializeField]
    private Stamina playerStamina;

    [SerializeField]
    private Health playerHealth;

    [SerializeField]
    private AudioSource _attack;



    // Start is called before the first frame update
    void Start()
    {
       
}

    public void AttackOption()
    {
        if (playerStamina.value < 5.0f)
        {
            _attackText.text = "Not Enough Stamina!";
        }
        else
        {
            if (_attackCooldown <= 0f)
            {
                _attackText.text = "Press F to Attack";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    playerStamina.subtract(2.5f);
                    BasicAttack(_currentEnemy);
                    _attack.PlayOneShot(_attack.clip);
                    _attackCooldown = 1f / attackSpeed;
                }
            }
            else _attackText.text = "Attack on cooldown";
        }
         
        
        
    }

    public void BasicAttack(GameObject enemy)
    {
        Debug.Log(enemy.name + " has been attacked");

        // Enemies have health and need to hit mulitiple times 
        // THIS WHOLE THING IS MESSY AND I NEED TO CHANGE HOW THE STATS WORK TO CLEAN IT UP
        
        enemy.GetComponent<TestSamuraiController>().health -= 1;
        

        if (enemy.GetComponent<TestSamuraiController>().health == 0)
        {
            _spawnManager.EnemyKilled(enemy);
            Destroy(enemy);
            _attackText.text = "";
        }


        // One Hit KO
        //_spawnManager.EnemyKilled(enemy);
        //Destroy(enemy);
        //_attackText.text = "";
    }

    public bool IsLooking(Transform target)
    {
        Vector3 dir = (target.position - transform.position).normalized;
        float dot = Vector3.Dot(dir, transform.forward);

        // Debug.Log("Dot product for " + target.gameObject.name + ": " + dot);
        if (dot >= 0.95f)
        {
            // Debug.Log("Looking at " + target.gameObject.name);
            _currentEnemy = target.gameObject;
            return true;
        }
        else return false;
    }

    // Update is called once per frame
    void Update()
    {
        _attackCooldown -= Time.deltaTime;

        if (_currentEnemy != null)
        {
            if (_currentEnemy.GetComponent<TestSamuraiController>().isLookedAt)
            {
                AttackOption();
            }
            else _attackText.text = "";
        }

        //if (playerHealth.value == 0)
        //{
        //   PlayerManager.instance.KillPlayer();
        //}

        dv = GameObject.FindGameObjectWithTag("windArea").gameObject;
        
        if(Input.GetKey(KeyCode.Q) || Input.GetKeyDown(KeyCode.Q))
        {
            dv.gameObject.GetComponent<WindArea>().dvC.enabled = true;
        }
        else
        {
            dv.gameObject.GetComponent<WindArea>().dvC.enabled = false;
        }

    }
    public IEnumerator MyMethod()
    {
        yield return new WaitForSeconds(1);
    }
}
