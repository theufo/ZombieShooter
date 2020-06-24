using Assets.Scripts;
using UnityEngine;

public class Zombie : Enemy, IEnemyStats
{
    public int MaxHealth { get; set; } = 20;
    public int CurrentHealth { get; set; }
    public float Damage { get; set; } = 10;

    private float _timeBetweenMeleeDamage = 1;
    private float _nextHit;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _nextHit = Time.time + _timeBetweenMeleeDamage;
            Player.DoDamage(Damage);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time < _nextHit)
                return;

            _nextHit = Time.time + _timeBetweenMeleeDamage;
            Player.DoDamage(Damage);
        }
    }
}