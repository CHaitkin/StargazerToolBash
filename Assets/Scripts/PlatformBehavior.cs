using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public float platformLife = 10;
    public float speed = 2;
    GameObject thisPlatform;
    Rigidbody2D thisRigidbody;
    PlatformGenerator platformGenerator;

    float platformLifeLeft;
    // Start is called before the first frame update
    void Start()
    {
        thisPlatform = this.gameObject;
        thisRigidbody = GetComponent<Rigidbody2D>();
        platformGenerator = GameObject.FindObjectOfType<PlatformGenerator>();

        //thisRigidbody.AddForce(Vector2.up, ForceMode2D.Impulse);
        thisRigidbody.velocity = Vector3.up;
        thisRigidbody.velocity *= speed;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController) && !collision.gameObject.TryGetComponent<ProjectileDestructor>(out ProjectileDestructor projectileDestructor))
        {
            Debug.Log($"{thisPlatform} hit {collision.gameObject.name}");
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
