using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    Player Player;
    NavMeshAgent NavMeshAgent;

    void Start()
    {
        Player = FindObjectOfType<Player>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        NavMeshAgent.SetDestination(Player.transform.position);
    }
}