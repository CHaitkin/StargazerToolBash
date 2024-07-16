using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Mover), typeof(Jumper), typeof(GroundDetector))]
public class PlayerController : MonoBehaviour
{
    // Hello programmer person! If you celebrate July 4th, I hope it was/will be a *blast*!
    //
    // Also, cheers to a fantastic Level 3! Personal advice from Dylan: try not to worry about the House Cup
    // too much. It's fun and all, but ultimately you are here to learn and grow! So don't take the cup too seriously.
    //
    // That said, it is still fun, so the first person to post that they found this little message in the
    // #level-3-cohort-7 channel and pings @Dylan! will get 5 House Cup Points! (Prefects are excluded since they got the project early)
    
    private Mover mover;
    private Jumper jumper;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private PlayerAnimations playerAnimations;
    private GroundDetector groundDetector;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private bool movingRight = false;



    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        moveAction = playerInput.Player.Move;
        moveAction.Enable();
        playerInput.Player.Jump.performed += OnJump;
        playerInput.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        playerInput.Player.Jump.performed -= OnJump;
        playerInput.Player.Jump.Disable();
    }

    public void Start()
    {
        mover = GetComponent<Mover>();
        jumper = GetComponent<Jumper>();
        groundDetector = GetComponent<GroundDetector>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame, around 60 times a second
    void Update()
    {
        ////Listen for key presses and move left
        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        //{
        //    mover.AccelerateInDirection(new Vector2(-1, 0));
        //    spriteRenderer.flipX = true;
        //}

        ////Listen for key presses and move right
        //if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        //{
        //    mover.AccelerateInDirection(new Vector2(1, 0));
        //    spriteRenderer.flipX = false;
        //}

        ////Listen for key presses and jump
        //if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        //{
        //    jumper.Jump();

        //    //Play a Jump Sound
        //    if (audioSource != null)
        //    {
        //        audioSource.Play();
        //    }
        //}
    }
    private void FixedUpdate()
    {
        if(GameManager.Instance.gameRunning)
        {
            Vector2 moveDirection = moveAction.ReadValue<Vector2>();
            if (moveDirection.x > 0)
            {
                mover.MoveInDirection(moveDirection);
                playerAnimations.StopMoveAnimation(false);
            }
            if (moveDirection.x < 0)
            {
                mover.MoveInDirection(moveDirection);
                playerAnimations.StopMoveAnimation(false);
            }
            if (moveDirection.x == 0)
            {
                playerAnimations.StopMoveAnimation(true);
            }
            if (moveDirection.x > 0 && !movingRight)
            {
                Flip();
            }
            else if (moveDirection.x < 0 && movingRight)
            {
                Flip();
            }
            playerAnimations.PlayJumpAnimation(groundDetector.IsGrounded());
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        jumper.Jump();
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }
    void Flip()
    {
        movingRight = !movingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
