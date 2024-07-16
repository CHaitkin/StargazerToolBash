using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FloodBehavior : MonoBehaviour
{
    public Transform floodTransform;
    public float floodTime = 1f;
    public float floodRate = 0.01f;
    public int damage = 100;
    private float floodScale = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("IncreaseFlood", 0, floodRate);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void IncreaseFlood()
    {
        floodScale += floodRate;
        floodTransform.localScale = new Vector2(40, floodScale);

        if (floodScale >= 170)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destructible destructible = collision.gameObject.GetComponent<Destructible>();

            destructible.TakeDamage(damage);
        }
    }
}
