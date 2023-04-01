using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public PlayerData playerData;


    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + playerData.money.ToString(); 
    }
}
