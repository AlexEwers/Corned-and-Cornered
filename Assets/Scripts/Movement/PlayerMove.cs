using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpForce = 100f;

    public Transform GroundCheck;
    public LayerMask GroundLayer;

    public GameEvent OnJump;

    private Rigidbody2D rb;

    private float horizontalInput;
    private float verticalInput;
    private bool verticalJumpInput;

    private Vector2 horizontalVector = Vector2.zero;
    private Vector2 verticalVector = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void GetInput()
    {
        horizontalInput =  Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        verticalJumpInput = Input.GetButton("Jump");
    }

    private void GetHorizontalMove()
    {
        horizontalVector.x = horizontalInput * Speed;
        horizontalVector.y = rb.velocity.y;
    }
    private void GetMoveVerticalMove()
    {
        verticalVector.x = rb.velocity.x;
        verticalVector.y = verticalInput;
    }

    private void GetVerticalMove()
    {
        if (HasGravity() && verticalJumpInput)
        {
            Jump();
        }
        else
        {
            GetMoveVerticalMove();
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            OnJump.Raise();
            rb.AddForce(Vector2.up * JumpForce);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.1f, GroundLayer);
    }

    private bool HasGravity()
    {
        if (rb.gravityScale > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Move()
    {
        if (HasGravity())
        {
            rb.velocity = horizontalVector;
        }
        else
        {
            rb.velocity = new Vector2(horizontalVector.x, verticalVector.y).normalized * Speed;
        }
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        GetHorizontalMove();
        GetVerticalMove();

        Move();
    }
}
