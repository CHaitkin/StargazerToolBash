using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5f; // Speed of the enemy
    public float stopDistance = 1f; // Distance to stop from the player
    public float distance; //Distance to check for the player
    public PlayerController player;
    public enum enemyStates { Patrol, Following, Attacking};
    public enemyStates currentEnemyState;
    public float detectionRadius = 10f;

    private void Start()
    {
        currentEnemyState = enemyStates.Patrol;
    }
    // Function to check if any player is within the detection radius
    public PlayerController IsPlayerInRange()
    {
        // Find all objects with the tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            // Check the distance between the enemy and each player
            if (Vector2.Distance(transform.parent.position, player.transform.position) <= detectionRadius)
            {
                return player.GetComponent<PlayerController>(); // Return the player if found within the detection radius
            }
        }

        return null; // Return null if no players are found within the detection radius
    }
    private void Update()
    {
        player = IsPlayerInRange();
        if (player == null) 
        {
            currentEnemyState = enemyStates.Patrol;
            ReturnToPatrol();
            Debug.Log("Patrolling");
            return;
        }
        // Calculate the distance to the player
        distance = Vector2.Distance(transform.position, player.transform.position);
        
        // Check if the distance is greater than the stop distance
        if(distance > stopDistance)
        {
            currentEnemyState = enemyStates.Following;
            FollowPlayer();
            Debug.Log("Following");
            return;
        }
        
        currentEnemyState = enemyStates.Attacking;
        Debug.Log("Attacking");


    }
    void FollowPlayer()
    {
        
        // Calculate the direction to move towards the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    void ReturnToPatrol()
    {

        // Calculate the direction to move towards the player
        Vector2 direction = (transform.position - transform.parent.position).normalized;

        // Move the enemy towards the player
        transform.position = Vector2.MoveTowards(transform.position, transform.parent.position, speed * Time.deltaTime);
    }
    //private float distanceFromPlayer;

    //private PlayerController player;

    //bool isAttacking = false;
    //bool isPatrolling = true;
    //bool isFollowing = false;
    //[SerializeField] bool canFollow;

    //private void Start()
    //{
    //    player = FindObjectOfType<PlayerController>();
    //}
    //private void Update()
    //{
    //    StateDetection();
    //    if(isPatrolling)
    //    {
    //        Patrol();
    //    }
    //    if(isFollowing)
    //    {
    //        Follow();
    //    }
    //    if(isAttacking)
    //    {
    //        Attack();
    //    }
    //}

    //void StateDetection()
    //{
    //    if (player != null)
    //    {
    //        distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
    //        Debug.Log("The Distance is: " + distanceFromPlayer);
    //    }
    //    if(distanceFromPlayer > 10 && !isFollowing)
    //    {
    //        isPatrolling = true;
    //        isAttacking = false;
    //    }
    //    if(distanceFromPlayer < 10 && !canFollow &&!isFollowing)
    //    {
    //        isPatrolling = false;
    //        isAttacking = true;
    //    }
    //    if(distanceFromPlayer < 10 && canFollow)
    //    {
    //        isPatrolling = false;
    //        isFollowing = true;
    //        isAttacking = false;
    //    }
    //}

    //void Patrol()
    //{
    //    Debug.Log("Patrolling");
    //}

    //void Follow()
    //{
    //    if(player != null)
    //    {
    //        Vector2 enemyPos = transform.position;
    //        transform.position = Vector2.MoveTowards(enemyPos, player.transform.position, 2f * Time.deltaTime);
    //        Debug.Log("Following " + player.transform.name);
    //        StartCoroutine(FollowTime());
    //    }
    //}

    //void Attack()
    //{
    //    Debug.Log("Attacking " + player.transform.name);
    //}

    //IEnumerator FollowTime()
    //{
    //    yield return new WaitForSeconds(5f);
    //    isFollowing = false;
    //    //canFollow = false;
    //    yield return new WaitForSeconds(0.1f);
    //    canFollow = true;
    //    isPatrolling = true;
    //}
}
