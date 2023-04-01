using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerData playerData;
    public TurretData turretData;
    Transform target;
    int damage = 40;
    float speed = 70f;
    float explosionRadius = 0f;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        damage = turretData.damage;
        speed = turretData.projectileSpeed;
        explosionRadius= turretData.explosionRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void HitTarget()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        if (explosionRadius > 0f) 
        {
            Explode();
        } else
        {
            Damage(target);
        }

        gameObject.SetActive(false);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        EnemyController e = enemy.GetComponent<EnemyController>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
        
    }
}
