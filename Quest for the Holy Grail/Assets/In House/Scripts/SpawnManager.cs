using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int _MaxEnemies = 6;

    [SerializeField]
    private GameObject[] _spawns;

    private float _spawnRate = 2.0f;
    
    

    private List<GameObject> _spawnedEnemies = new List<GameObject>();
    private int _spawnPoint = 0;
    public GameObject[] _enemies;
    private int _enemyIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnemyKilled(GameObject enemy)
    {
        _spawnedEnemies.Remove(enemy);
        
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(_spawnRate);


    }

    private void SpawnNewEnemy(Vector3 NewLocaton, Quaternion rotation, GameObject EnemyPrefab)
    {
        StartCoroutine(SpawnDelay());
        GameObject newEnemy = Instantiate(EnemyPrefab, NewLocaton, rotation);
        Debug.Log("Added new enemy at location " + _spawnPoint);
        _spawnedEnemies.Add(newEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        // need to detect collisions before spawning in case spawnPoints overlap with enemies

        if (_spawnedEnemies.Count < _MaxEnemies)
        {
            _spawnPoint = Random.Range(0, _spawns.Length);
            _enemyIndex = Random.Range(0, _enemies.Length);

            Vector3 location = new Vector3(_spawns[_spawnPoint].transform.position.x, _spawns[_spawnPoint].transform.position.y, _spawns[_spawnPoint].transform.position.z);

            SpawnNewEnemy(location, _spawns[_spawnPoint].transform.rotation, _enemies[_enemyIndex]);
            
        }
    }
}
