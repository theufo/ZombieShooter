using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SpawnerTestScript
    {
        [UnityTest]
        public IEnumerator CanEnemySpawn()
        {
            var enemyPrefab = Resources.Load<GameObject>("Test/Enemy");
            var spawner = new GameObject().AddComponent<EnemySpawner>();
            spawner.Construct(2, enemyPrefab);
            spawner.Spawn();

            yield return null;

            var gameObject = GameObject.FindWithTag("Enemy");
            Assert.NotNull(gameObject);
        }

        [TearDown]
        public void AfterTest()
        {
            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Object.Destroy(enemy);
            }
        }

        [UnityTest]
        public IEnumerator SpawnerTimerTest()
        {
            var enemyPrefab = Resources.Load<GameObject>("Test/Enemy");
            var spawner = new GameObject().AddComponent<EnemySpawner>();
            spawner.Construct(1, enemyPrefab);
            spawner.StartSpawn();

            yield return new WaitForSeconds(1);

            var gameObject = GameObject.FindWithTag("Enemy");
            Assert.NotNull(gameObject);
        }
    }
}