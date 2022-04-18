using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform Target { get; set; }
    public float speed = 70f;
    public GameObject impactEffect;
    
    
    // Update is called once per frame
    void Update()
    {
        if (Target==null)
        {
            Destroy(gameObject);
            return;
        }
        var dir = Target.position-transform.position;
        var distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude<=distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized*distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        var impactEffectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(impactEffectInstance,2f);
        Destroy(Target.gameObject);
        Destroy(gameObject);
    }
}
