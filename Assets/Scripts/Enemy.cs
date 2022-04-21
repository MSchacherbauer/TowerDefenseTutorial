using UnityEngine;

public class Enemy : MonoBehaviour
{
    public const string EnemyTag = "Enemy";
    public float speed = 10f;
    public int health = 100;
    public int reward = 50;
    private Transform _target;
    private int _waypointIndex;
    public GameObject deathEffect;

    private void Start()
    {
        _target = Waypoints.Points[0];
    }

    private void Update()
    {
        var direction = _target.position - transform.position;
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f) GetNextWaypoint();
    }

    public void TakeDamage(int damageAmount)
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
        var deathEffectInstance = Instantiate(deathEffect, transform.position,Quaternion.identity);
        Destroy(deathEffectInstance,2);
        Destroy(gameObject);
    }

    private void GetNextWaypoint()
    {
        if (_waypointIndex >= Waypoints.Points.Length - 1)
        {
            Destroy(gameObject);
            PlayerStats.Lives--;
            return;
        }

        _waypointIndex++;
        _target = Waypoints.Points[_waypointIndex];
    }
}