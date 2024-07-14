using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float distanceFromPlayer;

    private PlayerController player;
    
    bool isAttacking = false;
    bool isPatrolling = true;
    bool isFollowing = false;
    [SerializeField] bool canFollow;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        StateDetection();
        if(isPatrolling)
        {
            Patrol();
        }
        if(isFollowing)
        {
            Follow();
        }
        if(isAttacking)
        {
            Attack();
        }
    }

    void StateDetection()
    {
        if(player != null)
        {
            distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
            Debug.Log("The Distance is: " + distanceFromPlayer);
        }
        if(distanceFromPlayer > 2)
        {
            isPatrolling = true;
            isAttacking = false;
            isFollowing = false;
        }
        if(distanceFromPlayer < 2)
        {
            isPatrolling = false;
            isFollowing = false;
            isAttacking = true;
        }
        if(distanceFromPlayer < 2 && canFollow)
        {
            isPatrolling = false;
            isFollowing = true;
            isAttacking = false;
        }
    }

    void Patrol()
    {
        Debug.Log("Patrolling");
    }

    void Follow()
    {
        Debug.Log("Following " + player.transform.name);
    }

    void Attack()
    {
        Debug.Log("Attacking " + player.transform.name);
    }
}
