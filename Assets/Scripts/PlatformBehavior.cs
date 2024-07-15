using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public float platformLife = 10;
    GameObject thisPlatform;
    Rigidbody2D thisRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        thisPlatform = this.gameObject;
        thisRigidbody = GetComponent<Rigidbody2D>();

        thisRigidbody.AddForce(Vector2.up, ForceMode2D.Impulse);
        Destroy(thisPlatform, platformLife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController) && !collision.gameObject.TryGetComponent<ProjectileDestructor>(out ProjectileDestructor projectileDestructor))
        {
        Debug.Log($"{thisPlatform} hit {collision.gameObject.name}");
            Debug.Log("It's not a player!");
            Destroy(thisPlatform);
        }
    }
}
