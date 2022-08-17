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

    private int _randomPowerup;

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
            if (_enemyContainer.transform.childCount == 0 && i == 5)
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
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 powerupPosition = new Vector3(Random.Range(-18f, 18f), 11, 0);
            float powerupProbability = Random.value;
            if (powerupProbability < .1f)
            {
                _randomPowerup = 0; // TripleShot
            }
            else if (powerupProbability >= .1f && powerupProbability < .2f )
            {
                _randomPowerup = 1; // Speed Boost
            }
            else if (powerupProbability >= .2f && powerupProbability < .3f)
            {
                _randomPowerup = 2; // Shield
            }
            else if (powerupProbability >= .3f && powerupProbability < .6f)
            {
                _randomPowerup = 3; // Ammo
            }
            else if (powerupProbability >= .6 && powerupProbability < .7f)
            {
                _randomPowerup = 4; // health pack
            }
            else if (powerupProbability >= .7f && powerupProbability < .8f)
            {
                _randomPowerup = 5; // kitten cannonball
            }
            else if (powerupProbability >= .8f && powerupProbability < .9f)
            {
                _randomPowerup = 6; // slowdown
            }
            else if (powerupProbability >= .9f && powerupProbability <= .1f)
            {
                _randomPowerup = 7; // Homing Missile
            }
            GameObject tripleShot = Instantiate(_powerups[_randomPowerup], powerupPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
