using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{

    private Transform _target;
    private int _waypointIndex;
    private Enemy _enemy;
    
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _target = Waypoints.Points[0];
    }

    private void Update()
    {
        var direction = _target.position - transform.position;
        transform.Translate(direction.normalized * (_enemy.speed * Time.deltaTime), Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f) GetNextWaypoint();
        _enemy.speed = _enemy.defaultSpeed;
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
