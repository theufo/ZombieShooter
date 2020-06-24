using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Cursor _cursor;
    Shot _shot;
    NavMeshAgent _navMeshAgent;
    public float MoveSpeed;
    public Transform GunBarrel;
    CapsuleCollider _capsuleCollider;
    private HealthBarController _healthBarController;
    public float Health = 100;
    public float MaxHealth = 100;
    GameStateController _gameStateController;

    void Start()
    {
        _cursor = FindObjectOfType<Cursor>();
        _gameStateController = FindObjectOfType<GameStateController>();
        _shot = FindObjectOfType<Shot>();
        _healthBarController = FindObjectOfType<HealthBarController>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
    }

    void Update()
    {
        var dir = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
            dir.z = -1;
        if (Input.GetKey(KeyCode.RightArrow))
            dir.z = 1;
        if (Input.GetKey(KeyCode.UpArrow))
            dir.x = -1;
        if (Input.GetKey(KeyCode.DownArrow))
            dir.x = 1;
        _navMeshAgent.velocity = dir.normalized*MoveSpeed;

        var forward = _cursor.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));

        if (Input.GetMouseButtonDown(0))
        {
            var from = GunBarrel.transform.position;
            var targetHeight = _cursor.transform.position.y + _capsuleCollider.height / 2;
            var to = new Vector3(_cursor.transform.position.x, targetHeight, _cursor.transform.position.z);
            var direction = (to - from).normalized;

            RaycastHit hit;
            if(Physics.Raycast(from, direction, out hit, 100))
            {
                if (hit.transform != null) {
                    var zombie = hit.transform.GetComponent<Zombie>();
                    if(zombie != null)
                        zombie.Kill();
                }
                to = new Vector3(hit.point.x, targetHeight, hit.point.z);
            }
            else
            {
                to = from + direction * 100;
            }

            _shot.Show(from, to);
        }
    }

    public void DoDamage(float value)
    {
        Health -= value;
        _healthBarController.SetHealth(Health/MaxHealth);
        if (Health <= 0)
        {
            _gameStateController.LoseGame();
        }
    }
}