using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public PlayerData playerData;


    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + playerData.health.ToString(); 
    }
}
