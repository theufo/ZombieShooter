using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    EnemySpawner[] spawners;
    KillsController killsController;
    private int _enemiesToKill;
    private int _killedEnemies;

    public TextMeshProUGUI WinText;
    public TextMeshProUGUI LoseText;
    public Button RestartButton;

    void Start()
    {
        spawners = FindObjectsOfType<EnemySpawner>();
        killsController = FindObjectOfType<KillsController>();

        foreach(var spawner in spawners)
        {
            _enemiesToKill += spawner.TotalEnemiesToSpawn;
        }

        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
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