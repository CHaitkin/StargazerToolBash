using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileDestructor : MonoBehaviour
{
    private GameObject thisProjectile;
    public int damage = 1;
    public LayerMask enemyLayer;

    public float projectileLifespan = 2f;

    // Start is called before the first frame update
    void Start()
    {
        thisProjectile = this.gameObject;
        Destroy(thisProjectile, projectileLifespan);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyType1") || collision.gameObject.CompareTag("EnemyType2") || collision.gameObject.CompareTag("EnemyType2Right") || collision.gameObject.CompareTag("EnemyType2Left") || collision.gameObject.CompareTag("Projectile"))
        {
            return;
        }
        Destructible destructible = collision.gameObject.GetComponent<Destructible>();

        if (destructible != null)
        {
            //Deal damage to the Destructible we touched!
            destructible.TakeDamage(damage);
        }
        Debug.Log("collided with " + collision.gameObject.name);
        Die();
    }

    private void Die()
    {
        Destroy(thisProjectile);
    }
}
