using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    public float range = 15f;
    public float turnSpeed = 5f;
    public float fireRate = 1f;

    [Header("Unity Setup fields")]
    public Transform partToRotate;
    public GameObject projectile;
    public Transform firePoint;
    private float _fireCountdown;
    private Transform _target;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    private void Update()
    {
        if (_target == null) return;

        FaceTarget();
        if (_fireCountdown <= 0)
        {
            Shoot();
            _fireCountdown = 1f / fireRate;
        }

        _fireCountdown -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Shoot()
    {
        var bulletGameObject = Instantiate(projectile, firePoint.position, firePoint.rotation);
        var bullet = bulletGameObject.GetComponent<Bullet>();
        if (bullet != null) bullet.Target = _target;
    }

    private void FaceTarget()
    {
        var dir = _target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    private void UpdateTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag(Enemy.EnemyTag);
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