using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 15f;
    public string enemyTag = "Enemy";
    public Transform parteToRotate;
    public float turnSpeed = 5f;
    private Transform _target;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    private void Update()
    {
        if (_target == null) return;

        var dir = _target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(parteToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        parteToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void UpdateTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        var minDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                closestEnemy = enemy;
                minDistance = distance;
            }

            if (closestEnemy != null && minDistance <= range)
                _target = closestEnemy.transform;
            else
                _target = null;
        }
    }
}