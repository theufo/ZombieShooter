using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int TotalEnemiesToSpawn = 10;
    private int _spawnedEnemiesCount;
    public float Period;
    public GameObject Enemy;
    private float _timeUntilNextSpawn;
    private bool _started;
    
    void Start()
    {
        _timeUntilNextSpawn = Random.Range(0, Period);
    }

    public void Construct(float period, GameObject enemy)
    {
        Period = period;
        Enemy = enemy;
    }

    void Update()
    {
        if (!_started)
            return;

        if (TotalEnemiesToSpawn <= _spawnedEnemiesCount)
            return;

        _timeUntilNextSpawn = _timeUntilNextSpawn - Time.deltaTime;
        if(_timeUntilNextSpawn <= 0)
        {
            _timeUntilNextSpawn = Period;
            Spawn();
        }
    }

    public void Spawn()
    {
        Instantiate(Enemy, transform.position, transform.rotation);
        _spawnedEnemiesCount++;
    }

    public void StartSpawn()
    {
        _started = true;
    }

    public void StopSpawn()
    {
        _started = false;
    }
}
