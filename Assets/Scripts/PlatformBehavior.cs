using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public float platformLife = 30;
    //public float speed = 2;
    GameObject thisPlatform;
    Rigidbody2D thisRigidbody;
    PlatformGenerator platformGenerator;

    float platformLifeLeft;
    public Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        thisPlatform = this.gameObject;
        thisRigidbody = GetComponent<Rigidbody2D>();
        platformGenerator = GameObject.FindObjectOfType<PlatformGenerator>();
        velocity = new Vector2(0f, 1.1f);
        //velocity = new Vector2(thisPlatform.transform.position.x, thisPlatform.transform.position.y + .1f); 

        //thisRigidbody.AddForce(Vector2.up, ForceMode2D.Impulse);
        //thisRigidbody.velocity = Vector3.up;
        //thisRigidbody.velocity *= speed;

        platformLifeLeft = platformLife;

        //Destroy(thisPlatform, platformLife);
    }

    // Update is called once per frame
    void Update()
    {
        platformLifeLeft -= Time.deltaTime;

        if (platformLifeLeft <= 0)
        {
            Die();
        }

    }

    private void FixedUpdate()
    {
        thisRigidbody.MovePosition(thisRigidbody.position + velocity * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"I hit something in CollisionEnter {collision.gameObject.name}");
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Projectile")
        {
            //Debug.Log($"{thisPlatform} hit {collision.gameObject.name}");
            //Debug.Log("It's not a player!");
            Die();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log($"I hit something in CollisionStay {collision.gameObject.name}");
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Projectile")
        {
            //Debug.Log($"{thisPlatform} hit {collision.gameObject.name}");
            //Debug.Log("It's not a player!");
            Die();
        }
    }

    private void Die()
    {
            Destroy(thisPlatform);
            platformGenerator.maxPlatforms++;

    }
}
