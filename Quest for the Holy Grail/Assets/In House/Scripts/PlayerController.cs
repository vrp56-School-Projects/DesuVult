using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private bool _lookingAtEnemey = false;
    private GameObject _currentEnemy = null;

    [SerializeField]
    private TextMeshProUGUI _attackText;

    [SerializeField]
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AttackOption()
    {
        _attackText.text = "Press F to Attack";
        if (Input.GetKeyDown(KeyCode.F))
        {
            BasicAttack(_currentEnemy);
        }
    }

    public void BasicAttack(GameObject enemy)
    {
        Debug.Log(enemy.name + " has been attacked");
        _spawnManager.EnemyKilled(enemy);
        Destroy(enemy);
        _attackText.text = "";
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
        if (_currentEnemy != null)
        {
            if (_currentEnemy.GetComponent<TestSamuraiController>().isLookedAt)
            {
                AttackOption();
            }
            else _attackText.text = "";
        }
       
    }
}
