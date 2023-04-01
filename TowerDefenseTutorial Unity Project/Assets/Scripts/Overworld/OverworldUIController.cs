using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldUIController : MonoBehaviour
{
    public PlayerData playerData;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(playerData.ovMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(playerData.ovHealth);
    }
}
