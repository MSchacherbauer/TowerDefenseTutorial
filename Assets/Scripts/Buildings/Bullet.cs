using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public float speed = 70f;
    public GameObject impactEffect;
    [FormerlySerializedAs("ExplosionRadius")]
    public float explosionRadius = 5f;
    public int damage = 50;
    public Transform Target { get; set; }


    // Update is called once per frame
    private void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }
        var dir = Target.position - transform.position;
        var distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(Target);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void HitTarget()
    {
        var impactEffectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(impactEffectInstance, 2f);
        if (explosionRadius > 0f)
            Explode();
        else
            Damage();
        Destroy(gameObject);
    }

    private void Explode()
    {
        foreach (var objectsInSphere in Physics.OverlapSphere(transform.position, explosionRadius))
            if (objectsInSphere.tag.Equals(Enemy.EnemyTag))
                Damage();
    }

    private void Damage()
    {
        var enemy = Target.GetComponent<Enemy>();
        if (enemy != null) enemy.TakeDamage(damage);
    }
}