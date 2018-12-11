using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float runSpeed = 5f;

    [SerializeField]
    private float jumpSpeed = 5f;

    [SerializeField]
    private float climbSpeed = 5f;

    [SerializeField]
    private float ladderSlideSpeed = 0f;

    [SerializeField]
    private Vector2 deathKick = new Vector2(10f, 25f);

    private Rigidbody2D rigidBody2D;
    private CapsuleCollider2D capsuleCollider2D;
    private BoxCollider2D boxCollider2D;
    private Animator animator;

    private bool isAlive = true;
    private bool isRunning = false;
    private bool isJumping = false;
    private bool isClimbing = false;
    private bool isGrounded = false;
    private bool isOnLadder = false;
    private bool isDying = false;

    private int ground;
    private int ladder;
    private int enemy;
    private int hazards;

    private float initialGravityScale;


    private void ClimbLadder()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");

        Vector2 playerVelocity = new Vector2(rigidBody2D.velocity.x, controlThrow * climbSpeed);

        rigidBody2D.velocity = playerVelocity;

        animator.SetBool("Climbing", isClimbing);
    }

    private void Die()
    {
        isAlive = false;

        rigidBody2D.velocity = deathKick;

        animator.SetBool("Dying", isDying);

        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    private void FixedUpdate()
    {
        //isGrounded = boxCollider2D.IsTouchingLayers(ground);
        //isOnLadder = capsuleCollider2D.IsTouchingLayers(ladder);

        //if (isAlive)
        //{
        //    if (capsuleCollider2D.IsTouchingLayers(enemy) || capsuleCollider2D.IsTouchingLayers(hazards))
        //    {
        //        isDying = true;
        //    }

        //    if (isRunning)
        //    {
        //        FlipSprite();
        //        Run();
        //    }
        //    else
        //    {
        //        Idle();
        //    }

        //    if (isJumping && isGrounded)
        //    {
        //        Jump();
        //    }
        //    else
        //    {
        //        Idle();
        //    }

        //    if (isOnLadder)
        //    {
        //        rigidBody2D.gravityScale = ladderSlideSpeed;

        //        if (isClimbing)
        //        {
        //            ClimbLadder();
        //        }
        //    }
        //    else
        //    {
        //        Idle();
        //    }

        //    if (isDying)
        //    {
        //        Die();
        //    }
        //    else
        //    {
        //        Idle();
        //    }
        //}
    }

    private void FlipSprite()
    {
        if (rigidBody2D.velocity.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidBody2D.velocity.x), 1f);
        }
    }

    private void Idle()
    {
        animator.SetBool("Running", isRunning);
        animator.SetBool("Climbing", isClimbing);

        rigidBody2D.gravityScale = initialGravityScale;
    }

    private void Jump()
    {
        Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);

        rigidBody2D.velocity += jumpVelocity;
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rigidBody2D.velocity.y);

        rigidBody2D.velocity = playerVelocity;

        animator.SetBool("Running", isRunning);
    }

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        ground = LayerMask.GetMask("Ground");
        ladder = LayerMask.GetMask("Ladder");
        enemy = LayerMask.GetMask("Enemy");
        hazards = LayerMask.GetMask("Hazards");

        isRunning = false;

        initialGravityScale = rigidBody2D.gravityScale;
    }

    private void Update()
    {
        isGrounded = boxCollider2D.IsTouchingLayers(ground);
        isOnLadder = capsuleCollider2D.IsTouchingLayers(ladder);

        if (isAlive)
        {
            if (CrossPlatformInputManager.GetAxis("Horizontal") != 0)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded)
            {
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }

            if (CrossPlatformInputManager.GetAxis("Vertical") != 0 && isOnLadder)
            {
                isClimbing = true;
            }
            else
            {
                isClimbing = false;
            }
        }

        if (isAlive)
        {
            if (capsuleCollider2D.IsTouchingLayers(enemy) || capsuleCollider2D.IsTouchingLayers(hazards))
            {
                isDying = true;
            }

            if (isRunning)
            {
                FlipSprite();
                Run();
            }
            else
            {
                Idle();
            }

            if (isJumping && isGrounded)
            {
                Jump();
            }
            else
            {
                Idle();
            }

            if (isOnLadder)
            {
                rigidBody2D.gravityScale = ladderSlideSpeed;

                if (isClimbing)
                {
                    ClimbLadder();
                }
            }
            else
            {
                Idle();
            }

            if (isDying)
            {
                Die();
            }
            else
            {
                Idle();
            }
        }
    }
}