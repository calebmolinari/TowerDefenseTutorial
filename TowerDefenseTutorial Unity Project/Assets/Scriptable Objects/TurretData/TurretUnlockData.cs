using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "TurretUnlockData")]
public class TurretUnlockData : ScriptableObject
{
    public readonly bool basic;
    public bool missile;
    public bool laser;
}
