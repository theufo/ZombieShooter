using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        protected Player Player;
        NavMeshAgent navMeshAgent;
        CapsuleCollider capsuleCollider;
        //BoxCollider capsuleCollider;
        Animator animator;
        MovementAnimator movementAnimator;
        private ParticleSystem particleSystem;
        private DiedEventHandler diedEventHandler;
        GameStateController gameStateController;
        bool dead;
        public int Health=100;

        void Start()
        {
            Player = FindObjectOfType<Player>();
            gameStateController = FindObjectOfType<GameStateController>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            //capsuleCollider = GetComponent<BoxCollider>();
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

        public void DoDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                Kill();
        }

        public void Kill()
        {
            if (!dead)
            {
                gameStateController.AddKill();
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
}