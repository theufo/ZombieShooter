using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float Period;
    public GameObject Enemy;
    public float TimeUntilNextSpawn;
    
    void Start()
    {
        TimeUntilNextSpawn = Random.Range(0, Period);
    }

    // Update is called once per frame
    void Update()
    {
        TimeUntilNextSpawn = TimeUntilNextSpawn - Time.deltaTime;
        if(TimeUntilNextSpawn <= 0)
        {
            TimeUntilNextSpawn = Period;
            Instantiate(Enemy, transform.position, transform.rotation);
        }
    }
}
