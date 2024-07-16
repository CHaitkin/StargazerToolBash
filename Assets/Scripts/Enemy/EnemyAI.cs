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
        if(isFollowing && canFollow)
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
        if(distanceFromPlayer > 10 && !isFollowing)
        {
            isPatrolling = true;
            isAttacking = false;
        }
        if(distanceFromPlayer < 10 && !canFollow &&!isFollowing)
        {
            isPatrolling = false;
            isAttacking = true;
        }
        if(distanceFromPlayer < 10 && canFollow)
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
        if(player != null)
        {
            Vector2 enemyPos = transform.position;
            transform.position = Vector2.MoveTowards(enemyPos, player.transform.position, 2f * Time.deltaTime);
            Debug.Log("Following " + player.transform.name);
            StartCoroutine(FollowTime());
        }
    }

    void Attack()
    {
        Debug.Log("Attacking " + player.transform.name);
    }

    IEnumerator FollowTime()
    {
        yield return new WaitForSeconds(5f);
        isFollowing = false;
        canFollow = false;
        yield return new WaitForSeconds(0.1f);
        canFollow = true;
        isPatrolling = true;
    }
}
