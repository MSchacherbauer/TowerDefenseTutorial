using TMPro.Examples;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : MonoBehaviour
{
    [Header("General")]
    public float range = 15f;
    public float turnSpeed = 5f;
    private Transform _target;
    private Enemy _enemy;

    [Header("Unity Setup fields")]
    public Transform partToRotate;
    public Transform firePoint;
    [Header("Use Bullets (default)")]
    public float fireRate = 1f;
    public GameObject projectile;
    private float _fireCountdown;

    [Header("UseLaser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem laserImpact;
    public int damageOverTime = 50;
    private Light _impactLight;
    public float slowPercentage=.5f;


    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
        _impactLight = laserImpact.GetComponentInChildren<Light>();
    }

    private void Update()
    {
        if (_target == null)
        {
            if (useLaser)
            {
                lineRenderer.enabled = false;
                laserImpact.Pause();
                _impactLight.enabled = false;
            }
            return;
        }
        FaceTarget();
        if (useLaser)
        {
            Laser();
        }
        else
        {
            Shoot();
        }
    }

    private void Laser()
    {
        _enemy.TakeDamage(damageOverTime * Time.deltaTime);
        _enemy.Slow(slowPercentage);
        RenderLaser();
    }

    private void RenderLaser()
    {
        lineRenderer.enabled = true;
        var dir = firePoint.position - _target.position;
        laserImpact.transform.position = _target.position + dir.normalized;
        laserImpact.transform.rotation = Quaternion.LookRotation(dir);
        laserImpact.Play();
        _impactLight.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, _target.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Shoot()
    {
        if (_fireCountdown <= 0)
        {
            var bulletGameObject = Instantiate(projectile, firePoint.position, firePoint.rotation);
            var bullet = bulletGameObject.GetComponent<Bullet>();
            if (bullet != null) bullet.Target = _target;
            _fireCountdown = 1f / fireRate;
        }
        _fireCountdown -= Time.deltaTime;
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
            {
                _target = closestEnemy.transform;
                _enemy = _target.GetComponent<Enemy>();
            }
            else
                _target = null;
        }
    }
}