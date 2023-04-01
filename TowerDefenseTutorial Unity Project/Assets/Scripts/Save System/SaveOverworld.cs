using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class SaveOverworld
{
    //Overworld data
    public float[] ovPosition;

    public SaveOverworld(GameObject player)
    {
        //Overworld data
        ovPosition = new float[3];
        ovPosition[0] = player.transform.position.x;
        ovPosition[1] = player.transform.position.y;
        ovPosition[2] = player.transform.position.z;
    }
}
