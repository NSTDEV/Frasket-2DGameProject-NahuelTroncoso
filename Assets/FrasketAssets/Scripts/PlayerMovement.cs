using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 2f;
    public float jumpSpeed = 3.5f;
    private float fallMultiplier = 0.5f;
    private float lowJumpMultiplier = 1f;

    private bool isWalking = false;
    private bool isGrounded;
    public AudioSource jump, walk;

    private float timeSinceLastSound = 0f;
    public float soundInterval = 0.4f;

    Rigidbody2D rb2D;

    public SpriteRenderer spriteRenderer;
    public Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Fruit") || other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void HorizontalMoveSound()
    {
        if (!isWalking)
        {
            if (Time.time - timeSinceLastSound >= soundInterval)
            {
                walk.Play();
                timeSinceLastSound = Time.time;
            }
        }
        else
        {
            walk.Stop();
            isWalking = false;
        }
    }

    private void HorizontalMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        rb2D.velocity = new Vector2(horizontalInput * runSpeed, rb2D.velocity.y);

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
            HorizontalMoveSound();
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
            HorizontalMoveSound();
        }
        else
        {
            animator.SetBool("Run", false);
            isWalking = false;
        }
    }

    private void PlayerJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            isGrounded = false;
            isWalking = false;

            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
            animator.SetBool("Idle", false);

            jump.Play();
            walk.Stop();
        }

        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            animator.SetBool("Jump", false);
        }

        if (rb2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
        }
    }

    private void Update()
    {
        PlayerJump();
    }

    private void FixedUpdate()
    {
        HorizontalMove();
    }
}