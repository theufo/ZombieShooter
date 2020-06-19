using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    Cursor cursor;
    NavMeshAgent navMeshAgent;
    public float MoveSpeed;

    void Start()
    {
        cursor = FindObjectOfType<Cursor>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
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
        navMeshAgent.velocity = dir.normalized*MoveSpeed;

        var forward = cursor.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));
    }
}
