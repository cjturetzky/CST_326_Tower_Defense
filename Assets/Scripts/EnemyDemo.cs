using UnityEngine;
using System.Collections.Generic;

public class EnemyDemo : MonoBehaviour
{
    // todo #1 set up properties
    //   health, speed, coin worth
    //   waypoints
    //   delegate event for outside code to subscribe and be notified of enemy death
    public int health = 3;
    public float speed = 3f;
    public int coins = 3;

    public List<Transform> waypointList;

    private int targetWaypointIndex;

    public delegate void EnemyDied(EnemyDemo deadEnemy);
    public event EnemyDied OnEnemyDied;

    // NOTE! This code should work for any speed value (large or small)

    //-----------------------------------------------------------------------------
    void Start()
    {
        // todo #2
        //   Place our enemy at the starting waypoint
        transform.position = waypointList[0].position;
        targetWaypointIndex = 1;
    }

    //-----------------------------------------------------------------------------
    void Update()
    {
        // todo #3 Move towards the next waypoint
        Vector3 targetPosition = waypointList[targetWaypointIndex].position;
        Vector3 movementDir = (targetPosition - transform.position).normalized;

        Vector3 newPosition = transform.position;
        newPosition += movementDir * speed * Time.deltaTime;
        transform.position = newPosition;
        // todo #4 Check if destination reaches or passed and change target
        if((newPosition - targetPosition).magnitude < 0.1){
            TargetNextWaypoint();
        }

        bool enemyDied = false;
        if(enemyDied){
            OnEnemyDied?.Invoke(this);
        }
    }

    //-----------------------------------------------------------------------------
    private void TargetNextWaypoint()
    {
        if(targetWaypointIndex == waypointList.Capacity - 1){
            Debug.Log("Enemy made it to the tower!");
            Destroy(this.gameObject);
        }
        else{
            targetWaypointIndex++;
        }
    }
}
