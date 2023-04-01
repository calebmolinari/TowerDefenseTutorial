using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Turret", menuName = "TurretStats")]
public class TurretData : ScriptableObject
{
    [Header("Turret Info")]
    public Sprite turretSprite;
    public string turretName;
    public float range;
    public float fireRate;
    public float turnSpeed;

    [Header("Projectile Info")]
    public int damage;
    public float projectileSpeed;
    public float explosionRadius;
    public string projectileString;
   
    [Header("For Lasers")]
    public float slowRatio;
    public bool useLaser;

}
