using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    private Player player;
    EnemySpawner[] spawners;
    KillsController killsController;
    private int _enemiesToKill;
    private int _killedEnemies;

    public TextMeshProUGUI WinText;
    public TextMeshProUGUI LoseText;
    public Button RestartButton;

    [SerializeField]
    private int attackRadius = 10;

    private int enemyLayerMask = 1 << 9;

    public bool AttackDone;

    [SerializeField]
    private float fireRate = 1; //times per second

    private float lastAttackTime = 0;

    void Start()
    {
        player = FindObjectOfType<Player>();
        spawners = FindObjectsOfType<EnemySpawner>();
        killsController = FindObjectOfType<KillsController>();

        foreach(var spawner in spawners)
        {
            _enemiesToKill += spawner.TotalEnemiesToSpawn;
            spawner.StartSpawn();
        }

        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
    }

    public void LateUpdate()
    {
        var asd = GetEnemiesInRadius();
        var enemy = GetClosestEnemy(asd);
        var time = lastAttackTime + (1 / fireRate);
        if (enemy != null && time < Time.realtimeSinceStartup)
        {
            lastAttackTime = Time.realtimeSinceStartup;
            player.FireAtEnemy(enemy);
            AttackDone = true;
        }
    }

    public Collider[] GetEnemiesInRadius()
    {
         return Physics.OverlapSphere(player.gameObject.transform.position, attackRadius, enemyLayerMask);
    }

    public Transform GetClosestEnemy(Collider[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = player.transform.position;
        foreach (var c in enemies)
        {
            var t = c.transform;
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    public void AddKill()
    {
        _killedEnemies++;
        killsController.AddKill();

        if (_killedEnemies >= _enemiesToKill)
            WinGame();
    }

    public void RestartGame()
    {
        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);

        SceneManager.LoadScene("GameScene");
    }

    public void LoseGame()
    {
        LoseText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
    }

    private void WinGame()
    {
        WinText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
    }
}