using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _mineTurret;

    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerups;
    private int _waveCount = 1;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        UIManager uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        yield return new WaitForSeconds(3.0f);
        int i = 0;
        uimanager.UpdateWave(_waveCount);
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(1.0f);
            if (i <= 4)
            {
                i++;
                if (i % 2 == 0)
                {
                    GameObject mineTurret = Instantiate(_mineTurret);
                    mineTurret.transform.parent = _enemyContainer.transform;
                }
                GameObject newEnemy = Instantiate(_enemyPrefab);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(3f);
            }
            if (_enemyContainer.transform.childCount <= 1 && i == 4)
            {
                _stopSpawning = true;
                _waveCount++;
                StartCoroutine(WaveBreakRoutine());
            }
        }
    }
    
    IEnumerator WaveBreakRoutine()
    {
        yield return new WaitForSeconds(5f);
        _stopSpawning = false;
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 powerupPosition = new Vector3(Random.Range(-18f, 18f), 11, 0);
            int randomPowerup = Random.Range(0, 7);
            GameObject tripleShot = Instantiate(_powerups[randomPowerup], powerupPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
