using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 7f;
    private float jumpForce = 10f;
    private float movementInput;

    private bool isGrounded;
    private Transform feetPos;
    private float checkRadius = 0.3f;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    private float jumpTime = 0.2f;
    private bool isJumping;

    private bool isFacingRight;

    private float wallJumpTime = 0.15f;
    private float wallSlideSpeed = 0.3f;
    private float wallDistance = 0.55f;
    private bool isWallSliding = false;
    RaycastHit2D WallCheckHit;

    private float airJumpTime = 0.2f;

    public HealthBar healthBar;
    private int maxHealth = 100;
    private int currentHealth;

    void Awake()
    {
        whatIsGround = LayerMask.GetMask("Ground");

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feetPos = GameObject.FindGameObjectWithTag("playerFeet").transform;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void FixedUpdate()
    {
        movementInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementInput * speed, rb.velocity.y);
    }

    void Update()
    {
        // Turning
        if (movementInput > 0)
        {
            isFacingRight = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (movementInput < 0)
        {
            isFacingRight = false;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // Jump
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded && Input.GetKeyDown(KeyCode.W) || isWallSliding && Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }

        //Hold Jump
        if (Input.GetKey(KeyCode.W) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else
            {
                isJumping = false;
            }
            
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }

        // Wall Jump
        if (isFacingRight)
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, whatIsGround);
        } else
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, whatIsGround);
        }

        if (WallCheckHit && !isGrounded && movementInput != 0)
        {
            isWallSliding = true;
            airJumpTime = Time.time + wallJumpTime;
        }
        else if (airJumpTime < Time.time)
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
        }
    }
    /*
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key code: " + e.keyCode);
            Debug.Log("??: " + rb.velocity);
        }
    }
    */
}
