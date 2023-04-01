using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyMovement : MonoBehaviour
{
    public PlayerData playerData;
    private Transform target;
    [SerializeField] private int waypointIndex = 0;
    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        target = Waypoints.points[0];
    }

    void Update()
    {
        

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemyController.enemyData.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        enemyController.enemyData.speed = enemyController.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            ReachGoal();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    void ReachGoal()
    {
        enemyController.Die();
        playerData.health -= 5;
    }

    public void ResetWaypoints()
    {
        waypointIndex = 0;
        target = Waypoints.points[waypointIndex];
    }

}
