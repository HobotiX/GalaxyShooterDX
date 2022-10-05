using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _enemySpawnSpeed = 2f;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject _starField;
    [SerializeField]
    private float _starFieldSpeed = 2f;

    private bool _spawnStop = true;

    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnStarFieldRoutine());
    }

    public void StartSpawning() 
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnStarFieldRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new  WaitForSeconds(3.0f);
        while (_spawnStop) {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnSpeed);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new  WaitForSeconds(6.0f);
        while (_spawnStop) {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_powerups[(int)Random.Range(0,3)], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }

    IEnumerator SpawnStarFieldRoutine()
    {
        while(true) {
            Vector3 spawnPosition = new Vector3(0, 17, 0); 
            Instantiate(_starField, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(_starFieldSpeed);
        }
    }

    public void onPlayerDeath()
    {
        _spawnStop = false;
    }
}
