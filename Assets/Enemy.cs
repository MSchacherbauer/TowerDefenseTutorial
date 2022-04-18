using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform _target;
    private int _waypointIndex;

    private void Start()
    {
        _target = Waypoints.Points[0];
    }

    private void Update()
    {
        var direction = _target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f) GetNextWaypoint();
    }

    private void GetNextWaypoint()
    {
        if (_waypointIndex >= Waypoints.Points.Length)
        {
            Destroy(gameObject);
            return;
        }

        _waypointIndex++;
        _target = Waypoints.Points[_waypointIndex];
    }
}