using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public float speed;
    public float health;
    public float startHealth;
    public int value;
}
