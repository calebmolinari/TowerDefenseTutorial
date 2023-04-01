using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretInventoryButton : MonoBehaviour
{
    public TurretData turretData;
    public TurretUnlockData turretUnlockData;
    public GameObject lockImage;
    public Image buttonImage;
    [SerializeField] Image turretImage;


    

    private void Update()
    {
        if (InventoryUI.selectedTurretButton != this)
            ToggleButtonColor(0);
    }

    public void SelectButton()
    {
        if (InventoryUI.selectedTurretButton != this)
        {
            InventoryUI.selectedTurretButton = this;
            ToggleButtonColor(0.07f);
        }
        else
        {
            InventoryUI.selectedTurretButton = null;
            ToggleButtonColor(0);
        }
    }

    void ToggleButtonColor(float alpha)
    {
        Color tempColor = buttonImage.color;
        tempColor.a = alpha;
        buttonImage.color = tempColor;
    }

    private void OnEnable()
    {
        if (turretData.turretName == "Basic Turret")
        {
            lockImage.SetActive(false);
            turretImage.color = Color.white;
        } 

        if (turretData.turretName == "Missile Turret")
        {
            if (turretUnlockData.missile)
            {
                lockImage.SetActive(false);
                turretImage.color = Color.white;
            }
            else
            {
                lockImage.SetActive(true);
                turretImage.color = Color.gray;

            }
        }

        if (turretData.turretName == "Laser Turret")
        {
            if (turretUnlockData.laser)
            {
                lockImage.SetActive(false);
                turretImage.color = Color.white;
            }
            else
            {
                lockImage.SetActive(true);
                turretImage.color = Color.gray;

            }
        }
    }
}
