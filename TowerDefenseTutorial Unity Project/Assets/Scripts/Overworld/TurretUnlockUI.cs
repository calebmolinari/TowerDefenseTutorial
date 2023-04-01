using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretUnlockUI : MonoBehaviour
{
    public static TurretData turretData;
    [SerializeField] TextMeshProUGUI turretName;
    [SerializeField] Image turretImage;

    private void OnEnable()
    {
        turretName.text = turretData.name;
        turretImage.sprite = turretData.turretSprite;
    }

    public void ExitUI()
    {
        PauseUI.isPaused = false;
        this.gameObject.SetActive(false);
    }
}
