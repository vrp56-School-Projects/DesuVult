using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int _MaxEnemies = 6;
    [SerializeField]
    private float _spawnRate = 2.0f;
    [SerializeField]
    private GameObject[] _spawns;
    

    private List<GameObject> _spawnedEnemies = new List<GameObject>();
    private int _spawnPoint = 0;
    public GameObject[] _enemies;
    private int _enemyIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 7; i > 1; --i)
        {
            GameObject newEnemy = Instantiate(_enemies[_enemyIndex], new Vector3(_spawns[i].transform.position.x, _spawns[i].transform.position.y, _spawns[i].transform.position.z), _spawns[i].transform.rotation);
            Debug.Log("Added new enemy at location " + i);
            _spawnedEnemies.Add(newEnemy);
        }
    }

    public void EnemyKilled(GameObject enemy)
    {
        _spawnedEnemies.Remove(enemy);
        
    }

    // Update is called once per frame
    void Update()
    {
        _spawnPoint = Random.Range(0, _spawns.Length);
        _enemyIndex = Random.Range(0, _enemies.Length);

        // need to detect collisions before spawning in case spawnPoints overlap with enemies

        if (_spawnedEnemies.Count < _MaxEnemies)
        {
            GameObject newEnemy = Instantiate(_enemies[_enemyIndex], new Vector3(_spawns[_spawnPoint].transform.position.x, _spawns[_spawnPoint].transform.position.y, _spawns[_spawnPoint].transform.position.z), _spawns[_spawnPoint].transform.rotation);
            Debug.Log("Added new enemy at location " + _spawnPoint);
            _spawnedEnemies.Add(newEnemy);
        }
    }
}
