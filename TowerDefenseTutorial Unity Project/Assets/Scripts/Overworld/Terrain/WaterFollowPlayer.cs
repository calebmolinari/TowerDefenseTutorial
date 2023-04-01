using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFollowPlayer : MonoBehaviour
{
    Transform waterTransform;
    public Transform playerTransform;

    private void Start()
    {
        waterTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        waterTransform.position = new Vector3(playerTransform.position.x, waterTransform.position.y, playerTransform.position.z + 30);
    }
}
