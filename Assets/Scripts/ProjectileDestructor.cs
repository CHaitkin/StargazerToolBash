using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileDestructor : MonoBehaviour
{
    private GameObject thisProjectile;
    public int damage = 1;

    public float projectileLifespan = 2f;

    // Start is called before the first frame update
    void Start()
    {
        thisProjectile = this.gameObject;
        Destroy(thisProjectile, projectileLifespan);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collided with {collision.gameObject.name}");
        Destructible destructible = collision.gameObject.GetComponent<Destructible>();

        if (destructible != null)
        {
            //Deal damage to the Destructible we touched!
            destructible.TakeDamage(damage);
        }
        Die();
    }

    private void Die()
    {
        Destroy(thisProjectile);
    }
}
