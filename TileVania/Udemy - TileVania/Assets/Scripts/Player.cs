using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float ladderClimbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(-10, 20);

    Rigidbody2D rigidBody;
    BoxCollider2D bodyCollider;
    CapsuleCollider2D feetCollider;
    Animator animator;

    bool isAlive = true;
    bool isClimbing = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        feetCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isAlive)
        {
            Run();
            Jump();
            ClimbLadder();
            FlipDirection();
            Die();
        }
    }

    private void Run()
    {
        rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed, rigidBody.velocity.y);
        animator.SetBool("running", Mathf.Abs(rigidBody.velocity.x) > 0);
    }

    private void Jump()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Foreground"))) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            rigidBody.velocity += new Vector2(0, jumpForce);
        }
    }

    private void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("die");
            rigidBody.velocity = deathKick;
        }
    }

    private void ClimbLadder()
    {
        if (!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladders"))) // if not colliding with ladder layer
        {
            animator.SetBool("climbing", false);
            rigidBody.gravityScale = 1;
            isClimbing = false;
            return;
        }
        if (Input.GetAxis("Vertical") > 0 && !isClimbing) // if pressing up
        {
            isClimbing = true;
        }
        if (isClimbing)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Input.GetAxisRaw("Vertical") * ladderClimbSpeed);
            rigidBody.gravityScale = 0;
            animator.SetBool("climbing", Mathf.Abs(rigidBody.velocity.y) > 0);
        }
    }

    private void FlipDirection()
    {
        if (rigidBody.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
        else if (rigidBody.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }
    }
}
