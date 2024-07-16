using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator playerAnim;

    private void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();
    }
    public void PlayJumpAnimation(bool OnGround)
    {
        playerAnim.SetBool("Jumping", !OnGround);
    }
    public void PlayMoveAnimation(float moveValue)
    {
        playerAnim.SetFloat("MovingValue", moveValue);
    }
    public void StopMoveAnimation(bool moving)
    {
        playerAnim.SetBool("StopMoving", moving);
    }

}
