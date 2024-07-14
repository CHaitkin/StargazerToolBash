using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileDestructor : MonoBehaviour
{
    private GameObject thisProjectile;

    public float projectileLifespan = 2f;
    //private float projectileLifeLeft;

    // Start is called before the first frame update
    void Start()
    {
        thisProjectile = this.gameObject;
        //projectileLifeLeft = projectileLifespan;
        Debug.Log($"Started object {thisProjectile.name}");
        Destroy(thisProjectile, projectileLifespan);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I hit something");
        Die();
    }

    private void Die()
    {
        Destroy(thisProjectile);
    }
}
