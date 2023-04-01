using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public EnemyData enemyData;
    public PlayerData playerData;
    public GameObject deathEffect;

    [SerializeField] float health;
    float slowedSpeed;
    [HideInInspector]
    public float startSpeed;
    private Transform target;
    public EnemyMovement enemyMovement;
    [Header("Unity Stuff")]
    public Image healthBar;
    public float healthPercent;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        startSpeed = enemyData.speed;
        enemyData.health = enemyData.startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = enemyData.health;

       
    }

   

    public void TakeDamage(float damage)
    {
        enemyData.health -= damage;
        healthPercent = enemyData.health / enemyData.startHealth;
        healthBar.fillAmount = healthPercent;

        if (enemyData.health <= 0 && !isDead)
        {
            playerData.money += enemyData.value;
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        enemyMovement.ResetWaypoints();
        gameObject.SetActive(false);
        WaveSpawner.enemiesAlive--;
    }

   public void Slow(float slowRatio)
    {
        slowedSpeed = startSpeed * slowRatio;
        enemyData.speed = slowedSpeed;
    }
   

}
