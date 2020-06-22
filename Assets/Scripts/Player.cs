using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    Cursor cursor;
    Shot shot;
    NavMeshAgent navMeshAgent;
    public float MoveSpeed;
    public Transform GunBarrel;
    public CapsuleCollider capsuleCollider;

    void Start()
    {
        cursor = FindObjectOfType<Cursor>();
        shot = FindObjectOfType<Shot>();
        capsuleCollider = GetComponent<CapsuleCollider>();
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

        if (Input.GetMouseButtonDown(0))
        {
            var from = GunBarrel.transform.position;
            var targetHeight = cursor.transform.position.y + capsuleCollider.height / 2;
            var to = new Vector3(cursor.transform.position.x, targetHeight, cursor.transform.position.z);
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
                to = from = direction * 100;
            }

            shot.Show(from, to);
        }
    }
}
