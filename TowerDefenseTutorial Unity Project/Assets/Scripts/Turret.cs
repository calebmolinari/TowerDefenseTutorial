using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public TurretData turretData;
    private Transform target;
    BulletPooler bulletPooler;
    private EnemyController targetController;

    [Header("Attributes")]

    [SerializeField] string projectileString;
    float range = 15f;
    float turnSpeed = 10f;
    float fireRate = 1f;

    [Header("Laser Data")]
    bool useLaser = false;
    int damageRate = 30;
    float slowRatio = 0.5f;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    private float fireCountdown = 0f;
    public string enemyTag = "Enemy";
    public string enemyToughTag = "EnemyTough";
    public string enemyWeakTag = "EnemyWeak";
    public Transform partToRotate;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        bulletPooler = BulletPooler.Instance;
        projectileString = turretData.projectileString;
        range = turretData.range;
        turnSpeed = turretData.turnSpeed;
        fireRate = turretData.fireRate;
        useLaser = turretData.useLaser;
        damageRate = turretData.damage;
        slowRatio = turretData.slowRatio;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
                    
            }
            return;
        }
            

        //Target Lock
        LockOnTarget();
       
        if (useLaser)
        {
            Laser();
        } else
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

    }

    void Laser()
    {
        if (!target.gameObject.activeSelf) { return; }

        targetController.TakeDamage(damageRate * Time.deltaTime);
        targetController.Slow(slowRatio);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
            

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGO = bulletPooler.SpawnFromPool(projectileString, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemy1 = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject[] enemy2 = GameObject.FindGameObjectsWithTag(enemyToughTag);
        GameObject[] enemy3 = GameObject.FindGameObjectsWithTag(enemyWeakTag);
        GameObject[] enemyHold = enemy1.Concat(enemy2).ToArray();
        GameObject[] enemies = enemyHold.Concat(enemy3).ToArray();
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetController = nearestEnemy.GetComponent<EnemyController>();
        }
        else target = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
