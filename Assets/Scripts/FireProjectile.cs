using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{

    public Rigidbody2D projectile;
    public GameObject enemyObject;
    private Transform source;
    public float speed = 2f;
    public float fireCooldown = 2.0f;
    public float fireDelay = 0.05f;
    private float fireCooldownLeft;

    // Start is called before the first frame update
    void Start()
    {
        source = enemyObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        fireCooldownLeft -= Time.deltaTime;
        if (fireCooldownLeft <= 0f)
        {
            StartCoroutine(HandleFire());
            fireCooldownLeft = fireCooldown;
        }
    }

    private IEnumerator HandleFire()
    {
        EnemyPatrol enemyPatrol = enemyObject.GetComponent<EnemyPatrol>();

        Vector3 direction = enemyPatrol.fireDirection;

        Rigidbody2D newHorizonalProjectile;
        Rigidbody2D newDiagonalProjectile;
        if (direction.x < source.position.x)
        {
            if (!enemyObject.CompareTag("EnemyType2Left"))
            {
                //Debug.Log($"{source.name} Fire Left");
                newHorizonalProjectile = Instantiate(projectile, source.position, source.rotation) as Rigidbody2D;
              
                // Fire horizontal left
                if ( enemyObject.CompareTag("EnemyType1"))
                {
                    newHorizonalProjectile.velocity = new Vector2(direction.x, 0);
                }
                else
                {
                    newHorizonalProjectile.velocity = new Vector2(-direction.x, 0);
                }
                newHorizonalProjectile.velocity *= speed;
                if (!enemyObject.CompareTag("EnemyType1"))
                {
                    yield return new WaitForSeconds(fireDelay);
                    newDiagonalProjectile = Instantiate(projectile, source.position, source.rotation) as Rigidbody2D;
                    // Fire diagonally up
                    newDiagonalProjectile.velocity = new Vector2(-direction.x, direction.y);
                    newDiagonalProjectile.velocity *= speed;
                }
            }
        }
        else
        {
            if (!enemyObject.CompareTag("EnemyType2Right"))
            {
                newHorizonalProjectile = Instantiate(projectile, source.position, source.rotation) as Rigidbody2D;
                //Debug.Log($"{source.name} Fire Right");
                // Fire horizonal right
                if ( enemyObject.CompareTag("EnemyType2") || enemyObject.CompareTag("EnemyType1"))
                //if (enemyObject.CompareTag("EnemyType2") )
                {
                    newHorizonalProjectile.velocity = new Vector2(direction.x, 0);
                }
                else
                {
                    newHorizonalProjectile.velocity = new Vector2(-direction.x, 0);    
                }
                newHorizonalProjectile.velocity *= speed;
                if ( !enemyObject.CompareTag("EnemyType1"))
                {
                    yield return new WaitForSeconds(fireDelay);
                    newDiagonalProjectile = Instantiate(projectile, source.position, source.rotation) as Rigidbody2D;
                    // Fire diagonally up
                    if (enemyObject.CompareTag("EnemyType2") || enemyObject.CompareTag("EnemyType1"))
                    {
                        newDiagonalProjectile.velocity = new Vector2(direction.x, direction.y);
                    }
                    else
                    {
                        newDiagonalProjectile.velocity = new Vector2(-direction.x, direction.y);
                    }
                    newDiagonalProjectile.velocity *= speed;
                }
            }
        }
    }
}
