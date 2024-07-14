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
    private float fireCooldownLeft;

    private bool isFiring = false;
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
            Debug.Log($"I have to shoot");
            FireHorizontal();
            fireCooldownLeft = fireCooldown;
        }
    }

    void FireHorizontal()
    {
        EnemyPatrol enemyPatrol = enemyObject.GetComponent<EnemyPatrol>();

        Transform direction = enemyPatrol.destinationPoint;

        Rigidbody2D newProjectile;
        newProjectile = Instantiate(projectile, source.position, source.rotation) as Rigidbody2D;
        if (direction.position.x > source.position.x)
        {
            //newProjectile.AddForce(force: speed * Time.deltaTime * newProjectile.transform.right);
            newProjectile.velocity = -direction.position * speed;

            Debug.Log("I am facing Right");
            //Debug.Log($"");
        }
        else
        {
            Debug.Log("I am facing left");
            //newProjectile.AddForce(-newProjectile.transform.right * speed * Time.deltaTime);
            newProjectile.velocity = direction.position * speed;
        }
        //Physics.IgnoreCollision(enemyObject.GetComponent<Collider>(), newProjectile.GetComponent<Collider>());

    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"I have collided with something");
        // Check if collision is with enemy layer
        if(collision.gameObject.layer == 7)
        {
            Debug.Log("Ignore collision on enemy layer");
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
    */
}
