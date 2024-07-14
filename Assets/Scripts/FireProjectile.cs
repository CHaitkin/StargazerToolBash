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

        Transform direction = enemyPatrol.destinationPoint;

        Rigidbody2D newHorizonalProjectile;
        Rigidbody2D newDiagonalProjectile;
        newHorizonalProjectile = Instantiate(projectile, source.position, source.rotation) as Rigidbody2D;
        newDiagonalProjectile = Instantiate(projectile, source.position, source.rotation) as Rigidbody2D;
        if (direction.position.x > source.position.x)
        {
            // Fire horizontal right
            newHorizonalProjectile.velocity = new Vector2(-direction.position.x, 0);
            newHorizonalProjectile.velocity *= speed * Time.timeScale;
            //newHorizonalProjectile.AddForce(direction.right * speed);
            yield return new WaitForSeconds(fireDelay);
            // Fire diagonally up
            newDiagonalProjectile.velocity = new Vector2(-direction.position.x, direction.position.y);
            newDiagonalProjectile.velocity *= speed * Time.timeScale;


        }
        else
        {
            // Fire horizonal left
            newHorizonalProjectile.velocity = new Vector2(direction.position.x, 0);
            newHorizonalProjectile.velocity *= speed * Time.timeScale;
            yield return new WaitForSeconds(fireDelay);
            // Fire diagonally up
            newDiagonalProjectile.velocity = new Vector2(direction.position.x, direction.position.y);
            newDiagonalProjectile.velocity *= speed * Time.timeScale;
        }
    }

}
