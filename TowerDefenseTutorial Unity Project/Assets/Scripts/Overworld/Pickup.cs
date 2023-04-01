using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject turretUnlockPanel;
    public TurretData turretData;
    public TurretUnlockData turretUnlockData;
    bool swap;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        
        PauseUI.isPaused = true;
        TurretUnlockUI.turretData = turretData;

        swap = turretUnlockData.missile;
        turretUnlockData.missile = !swap;

        turretUnlockPanel.SetActive(true);
    }
}
