using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public const string EnemyTag = "Enemy";
    public float defaultSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float health = 100;
    public int reward = 50;
    public GameObject deathEffect;

    private void Start()
    {
        speed = defaultSpeed;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerStats.Money += reward;
        var deathEffectInstance = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffectInstance, 2);
        Destroy(gameObject);
    }

    public void Slow(float slowPercentage)
    {
        speed = defaultSpeed * (1 - slowPercentage);
    }
}