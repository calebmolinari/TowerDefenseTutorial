using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStation : MonoBehaviour
{
    public PlayerData playerData;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);

        playerData.health = playerData.maxHealth;
    }
}
