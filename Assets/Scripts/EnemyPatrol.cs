using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    private int currentWaypointIndex = 0;
    public Vector3 fireDirection;
    private bool movingRight = false;

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (waypoints.Length == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        fireDirection = (targetWaypoint.position - transform.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Check if the enemy reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            fireDirection = (waypoints[currentWaypointIndex].position - transform.position);
            Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

            // Flip the enemy based on the direction
            if (direction.x > 0 && !movingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && movingRight)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}