using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";

    private float horizontal;
    
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool isRunning = false;
    private Animator animator;
    private Rigidbody2D rb;
    private int jumpCount;
    private int jumpCountMax = 2;
    private float speed;
    private float sprintSpeed;


    [SerializeField] private Transform playerGroundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask finishLayer;
    [SerializeField] private LayerMask coinLayer;

    [SerializeField] private float normalSpeed = 8f;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private CoinPickup coinPickup;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        sprintSpeed = normalSpeed * 2f;
        speed = normalSpeed;

    }
    private void Update()
    {
        Walk();
        Sprint();
        Jump();
        Duck();
        Flip();
        Finish();
        if (isRunning)
        {
            speed = sprintSpeed;
            
        }
        else
        {
            speed = normalSpeed;
        }
        
    }

    private void FixedUpdate()
    {

        
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        
    }

    private void Walk()
    {
        horizontal = Input.GetAxisRaw(HORIZONTAL);
        if (horizontal < 0f || horizontal > 0f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded() || Input.GetButtonDown("Jump") && jumpCount < jumpCountMax)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetBool("IsJumping", true);
            jumpCount++;
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
        if (IsGrounded() && jumpCount == jumpCountMax)
        {
            jumpCount = 0;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(playerGroundCheck.position, 0.2f, groundLayer);
        

    }

    private bool HasFinished()
    {
        return Physics2D.OverlapCircle(playerGroundCheck.position, 0.2f, finishLayer);
    }

    private void Finish()
    {
        if (HasFinished())
        {
            Debug.Log("Player has finished");
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Duck()
    {
        if (Input.GetButtonDown("Duck"))
        {
            animator.SetBool("IsDucking", true);
        }
        else
        {
            animator.SetBool("IsDucking", false);
        }
    }

    private void Sprint()
    {
        if (Input.GetButton("Sprint") && !isRunning)
        {
            isRunning = true;
            animator.SetBool("IsRunning", true);
            
        }
        else
        {
            isRunning = false;
            animator.SetBool("IsRunning", false);     
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            gameManager.Checkpoint();
            
        }
        
    }

    private bool IsCoin()
    {
        Destroy(coinPickup);
        return Physics2D.OverlapCircle(playerGroundCheck.position, 0.2f, coinLayer);
    }
}
