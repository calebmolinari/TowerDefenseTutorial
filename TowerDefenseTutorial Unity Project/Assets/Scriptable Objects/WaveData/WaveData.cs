using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "WaveData")]
public class WaveData : ScriptableObject
{
    public int startingMoney;
    public float timeBetweenEnemies;
    public int[] waveSize;
    public string[] enemyTags;

}
