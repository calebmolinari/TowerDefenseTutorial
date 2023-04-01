using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public TextMeshProUGUI standardTurretCost;
    public TextMeshProUGUI missileTurretCost;
    public Button missileTurretButton;
    public Image missileTurretImage;
    public TextMeshProUGUI laserTurretCost;
    public Button laserTurretButton;
    public Image laserTurretImage;
    public Shop shop;
    public TurretUnlockData turretUnlockData;
    public TurretData missileData;
    public TurretData laserData;

    
    void Start()
    {
        standardTurretCost.text = "$" + shop.standardTurret.cost.ToString();
        missileTurretCost.text = "$" + shop.missileTurret.cost.ToString();
        laserTurretCost.text = "$" + shop.laserTurret.cost.ToString();

        if (turretUnlockData.missile)
        {
            missileTurretButton.enabled = true;
            missileTurretImage.color = Color.white;
        } 
        else
        {
            missileTurretButton.enabled = false;
            missileTurretImage.color = Color.gray;
        }

        if (turretUnlockData.laser)
        {
            laserTurretButton.enabled = true;
            laserTurretImage.color = Color.white;
        }
        else
        {
            laserTurretButton.enabled = false;
            laserTurretImage.color = Color.gray;
        }
    }
}
