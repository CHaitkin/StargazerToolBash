using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{

    //This tells us whether we are currently touching the ground
    //[HideInInspector]
    //public bool onGround = false;



    /*
     * Legacy from provided example template:
     * 
    // Note to Level 3 Cohort 7: This script is HORRIBLE! ABSOLUTELY ATROCIOUS! And that's on purpose! It's simple and easy to understand.
    // There are several bugs involved with this very simplistic approach to detecting the floor.
    // Try to improve it if you have time! Here's a hint: https://docs.unity3d.com/ScriptReference/Physics2D.Raycast.html
 
    //If we are touching ANYTHING, we are considered "on the ground"
    public void OnCollisionStay2D(Collision2D collision)
    {
        onGround = true;
    }

    //Whenever we STOP touching something, we are no longer considered "on the ground"
    public void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }
    */

    public float floatHeight;
    public float liftForce;
    public float damping;

    Rigidbody2D thisRigidBody2D;
    Collider2D thisCollider2D;

    private void Start()
    {
        thisRigidBody2D = GetComponent<Rigidbody2D>();
        thisCollider2D = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
    }

    public bool IsGrounded()
    {
        // Takes the calculated bottom of the identified collider and offsets the detection to prevent colliding with the source object
        Vector2 detectorStart = new(thisCollider2D.bounds.center.x, thisCollider2D.bounds.min.y + -0.1f);
        // Cast a ray straight down
        // We're setting a limit of the bottom of the collider
        RaycastHit2D detect = Physics2D.Raycast(detectorStart, Vector2.down, thisCollider2D.bounds.min.y);

        // This is a bit 'hacky' but checks to ensure we're not colliding with our own rigidbody
        // The bug that exists is if we land on 'enemy' we will be able to jump again (provided we don't die)
        // This would work better with a GroundLayer defined
        if (detect.collider != null && detect.collider.gameObject.name != thisRigidBody2D.gameObject.name)
        {
            return true;
        }
        return false;
    }
}
