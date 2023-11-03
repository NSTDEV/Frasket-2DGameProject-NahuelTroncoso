using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 2f;
    public float jumpSpeed = 3.5f;
    private float fallMultiplier = 0.5f;
    private float lowJumpMultiplier = 1f;

    Rigidbody2D rb2D;

    public SpriteRenderer spriteRenderer;
    public Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Fruits"));
    }

    private void playerMovement()
    {
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("Run", false);
        }
    }

    private void playerJump()
    {
        if (CheckGround.isGrounded == false)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
            animator.SetBool("Idle", false);
        }

        if (CheckGround.isGrounded == true)
        {
            animator.SetBool("Jump", false);
        }

        if (Input.GetKey("space") && CheckGround.isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }

        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        }

        if (rb2D.velocity.y > 0 && !Input.GetKey("space"))
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        playerMovement();
        playerJump();
    }
};