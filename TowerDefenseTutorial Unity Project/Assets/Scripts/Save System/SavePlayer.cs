using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavePlayer
{
    //Player data
    public int health;
    public int maxHealth;

    public SavePlayer(PlayerData playerData)
    {
        health = playerData.health;
        maxHealth = playerData.maxHealth;
    }
}
