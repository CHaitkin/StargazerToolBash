using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftPoint;
    public GameObject rightPoint;
    public float speed;
    public Transform destinationPoint;

    private Rigidbody2D enemyRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        destinationPoint = rightPoint.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Sets the enemy direction it will move toward to the destinationPoint
        Vector2 pointDirection = destinationPoint.position - transform.position;

        if (destinationPoint == rightPoint.transform)
        {
            // Moves enemy to the right
            enemyRigidbody.velocity = new Vector2(speed, 0);
        }
        else
        {
            // Moves enemy to the left
            enemyRigidbody.velocity = new Vector2(-speed, 0);
        }

        // Check if we have reached our right point
        if (Vector2.Distance(transform.position, destinationPoint.position) < 0.2f && destinationPoint == rightPoint.transform)
        {
            // We have reached the right point so change destinationPoint to leftPoint
            destinationPoint = leftPoint.transform;
        }
        if (Vector2.Distance(transform.position, destinationPoint.position) < 0.2f && destinationPoint == leftPoint.transform)
        {
            destinationPoint = rightPoint.transform;
        }
    }
}