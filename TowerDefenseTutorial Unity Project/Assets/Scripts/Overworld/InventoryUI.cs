using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static TurretInventoryButton selectedTurretButton;
    [Header("Selected Turret Panel")]
    public GameObject selectedTurretPanel;
    public TextMeshProUGUI turretName;
    public TextMeshProUGUI turretData;
    public TextMeshProUGUI turretDescription;
    public Image turretImage;
    private void Start()
    {
        selectedTurretPanel.SetActive(false);
    }

    public void TogglePanelSelectedTurret()
    {
        if (selectedTurretButton == null)
        {
            selectedTurretPanel.SetActive(false);
        }
        else
        {
            selectedTurretPanel.SetActive(true);
            turretImage.sprite = selectedTurretButton.turretData.turretSprite;
            turretName.text = selectedTurretButton.turretData.turretName;
            SetTurretDataText();
            SetTurretDescriptionText();
        }
    }    

    void SetTurretDataText()
    {
        turretData.text = "Damage: " + selectedTurretButton.turretData.damage + "\nRange: " + selectedTurretButton.turretData.range + "\nFire Rate: " + selectedTurretButton.turretData.fireRate
            + "\nExplosion Radius: " + selectedTurretButton.turretData.explosionRadius;
    }

    void SetTurretDescriptionText()
    {

    }
}
