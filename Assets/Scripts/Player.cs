using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public FloatingJoystick variableJoystick;

    Cursor _cursor;
    private bool autoFire;
    Shot _shot;
    NavMeshAgent _navMeshAgent;
    public float MoveSpeed;
    public Transform GunBarrel;
    CapsuleCollider _capsuleCollider;
    private HealthBarController _healthBarController;
    public float Health = 100;
    public float MaxHealth = 100;
    GameStateController _gameStateController;

    public int Attack = 20;

    void Start()
    {
        autoFire = true;
        _cursor = FindObjectOfType<Cursor>();
        _gameStateController = FindObjectOfType<GameStateController>();
        _shot = FindObjectOfType<Shot>();
        _healthBarController = FindObjectOfType<HealthBarController>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
    }


    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        var dir = Vector3.zero;
        dir.z = variableJoystick.Horizontal;
        dir.x = -variableJoystick.Vertical;


        //if (Input.GetKey(KeyCode.LeftArrow))
        //    dir.z = -1;
        //if (Input.GetKey(KeyCode.RightArrow))
        //    dir.z = 1;
        //if (Input.GetKey(KeyCode.UpArrow))
        //    dir.x = -1;
        //if (Input.GetKey(KeyCode.DownArrow))
        //    dir.x = 1;
        _navMeshAgent.velocity = dir.normalized * MoveSpeed;
    }

    //void Update()
    //{
    //    var dir = Vector3.zero;
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //        dir.z = -1;
    //    if (Input.GetKey(KeyCode.RightArrow))
    //        dir.z = 1;
    //    if (Input.GetKey(KeyCode.UpArrow))
    //        dir.x = -1;
    //    if (Input.GetKey(KeyCode.DownArrow))
    //        dir.x = 1;
    //    _navMeshAgent.velocity = dir.normalized * MoveSpeed;

    //    //var forward = _cursor.transform.position - transform.position;
    //    //transform.rotation = Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));

    //    //if (!autoFire && Input.GetMouseButtonDown(0))
    //    //{
    //    //    var from = GunBarrel.transform.position;
    //    //    var targetHeight = _cursor.transform.position.y + _capsuleCollider.height / 2;
    //    //    var to = new Vector3(_cursor.transform.position.x, targetHeight, _cursor.transform.position.z);
    //    //    var direction = (to - from).normalized;

    //    //    RaycastHit hit;
    //    //    if (Physics.Raycast(from, direction, out hit, 100))
    //    //    {
    //    //        if (hit.transform != null)
    //    //        {
    //    //            var zombie = hit.transform.GetComponent<Zombie>();
    //    //            if (zombie != null)
    //    //                zombie.Kill();
    //    //        }
    //    //        to = new Vector3(hit.point.x, targetHeight, hit.point.z);
    //    //    }
    //    //    else
    //    //    {
    //    //        to = from + direction * 100;
    //    //    }

    //    //    _shot.Show(from, to);
    //    //}
    //}

    public void FireAtEnemy(Transform enemy)
    {
        var from = GunBarrel.transform.position;
        var to = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);
        var forward = enemy.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));

        _shot.Show(from, to);

        enemy.GetComponent<Zombie>().DoDamage(Attack);
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