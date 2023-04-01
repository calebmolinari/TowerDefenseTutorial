using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "PlayerStats")]
public class PlayerData : ScriptableObject
{
    [Header("Tower Defense Data")]
    public int money;
    public int health;
    public int maxHealth;

    [Header("Overworld Data")]
    public float ovHealth;
    public float ovMaxHealth;
    public float ovHealthDrainRate;
}
