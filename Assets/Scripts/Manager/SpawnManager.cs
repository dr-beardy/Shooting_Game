using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float _intervalTimeEnemy = 3f;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerups;

    private bool _stopSpawning;

    float randomX;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTripleShotPowerup());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (!_stopSpawning)
        {
            GameObject enemy = Instantiate(_enemyPrefab, RandomPos(), Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(_intervalTimeEnemy);
        }
    }

    IEnumerator SpawnTripleShotPowerup()
    {
        while (!_stopSpawning)
        {
            float interval = Random.Range(6.0f, 15.0f);
            yield return new WaitForSeconds(interval);
            int randomIndexPowerups = Random.Range(0, 3);
            Instantiate(_powerups[randomIndexPowerups], RandomPos(), Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    Vector3 RandomPos()
    {
        randomX = Random.Range(-2.5f, 2.5f);
        return new Vector3(randomX, 7.0f, 0);
    }


} // class





















