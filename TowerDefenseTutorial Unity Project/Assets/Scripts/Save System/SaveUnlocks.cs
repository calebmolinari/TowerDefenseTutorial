using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class SaveUnlocks
{
    //Turret unlocks
    public bool missile;
    public bool laser;

    public SaveUnlocks(TurretUnlockData unlockData)
    {
        //Unlock data
        missile = unlockData.missile;
        laser = unlockData.laser;
    }
}
