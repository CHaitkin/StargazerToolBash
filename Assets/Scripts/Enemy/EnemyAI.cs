using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float distanceFromPlayer;

    private PlayerController player;
    
    bool isAttacking;
    bool isPatrolling;
    bool isFollowing;

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
    }

    void Patrol()
    {

    }

    void Follow()
    {

    }

    void Attack()
    {

    }
}
