using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerups;

    public void StartSpawnig()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 powerupPosition = new Vector3(Random.Range(-18f, 18f), 11, 0);
            int randomPowerup = Random.Range(0, 4);
            GameObject tripleShot = Instantiate(_powerups[randomPowerup], powerupPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
