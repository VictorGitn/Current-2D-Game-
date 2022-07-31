using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConroller : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private LayerMask WhatIsGround;
    private float groundRadius = 0.2f;
    private float currentSpeed;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        currentSpeed = _speed;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Move();
        GroundCheck();
    }
    private void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Atack();
        }
    }

    private void Move()
    {
        var xInput = Input.GetAxis("Horizontal");
        _animator.SetFloat("Speed", Mathf.Abs(xInput));
        //if (isGrounded)
        //{
        _rigidbody2D.velocity = new Vector2(xInput * currentSpeed, _rigidbody2D.velocity.y);
        //}
        if (xInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (xInput < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void ResetSpeed()
    {
        currentSpeed = _speed;
    }

    private void Jump()
    {
        _animator.SetTrigger("Jump");
        _rigidbody2D.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
    }

    private void Atack()
    {
        if (isGrounded)
        {
            currentSpeed = 0;
            _animator.SetTrigger("Atack");
        }
    }


    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, groundRadius, WhatIsGround);
        _animator.SetBool("isGround", isGrounded);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}
