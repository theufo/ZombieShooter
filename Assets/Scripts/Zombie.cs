using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    Player Player;
    NavMeshAgent navMeshAgent;
    CapsuleCollider capsuleCollider;
    Animator animator;
    MovementAnimator movementAnimator;
    private ParticleSystem particleSystem;
    private DiedEventHandler diedEventHandler;
    bool dead;

    void Start()
    {
        Player = FindObjectOfType<Player>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
        movementAnimator = GetComponentInChildren<MovementAnimator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        diedEventHandler = GetComponentInChildren<DiedEventHandler>();
        diedEventHandler.Died += DestroyGameObject;

    }

    void Update()
    {
        if (dead)
            return;

        navMeshAgent.SetDestination(Player.transform.position);
    }

    public void Kill()
    {
        if (!dead)
        {
            dead = true;
            particleSystem.Play();
            Destroy(capsuleCollider);
            Destroy(movementAnimator);
            Destroy(navMeshAgent);
            animator.SetTrigger("died");
        }
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}