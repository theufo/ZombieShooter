using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int TotalEnemiesToSpawn = 1;
    private int spawnedEnemiesCount;
    public float Period;
    public GameObject Enemy;
    public float TimeUntilNextSpawn;
    
    void Start()
    {
        TimeUntilNextSpawn = Random.Range(0, Period);
    }

    void Update()
    {
        if (TotalEnemiesToSpawn <= spawnedEnemiesCount)
            return;

        TimeUntilNextSpawn = TimeUntilNextSpawn - Time.deltaTime;
        if(TimeUntilNextSpawn <= 0)
        {
            TimeUntilNextSpawn = Period;
            Instantiate(Enemy, transform.position, transform.rotation);
            spawnedEnemiesCount++;
        }
    }
}
