using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyMan : Enemy
{
    [SerializeField] bool isFacingRight = true;
    [SerializeField] float _raycastingDistance = 1f;
    [SerializeField] string terrainTag;
    [SerializeField] private LayerMask WhatIsGround;


    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float speed;
    public override float Speed {
        get { return speed; }
        set { speed = value; } 
    }
    private int health;
    public override int Health {
        get { return health; }
        set { health = value; }
    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        Patrol();
    }

    public void Patrol()
    {
        CheckForWalls();
        CheckForHoles();
        Move();
    }
    public override void Move()
    {
        Vector3 directionTranslation = (isFacingRight) ? transform.right : -transform.right;
        directionTranslation *= Time.deltaTime * speed;
        _animator.SetFloat("Speed", speed);
        transform.Translate(directionTranslation);
    }

    private void CheckForWalls()
    {
        Vector3 raycastDirection = (isFacingRight) ? Vector3.right : Vector3.left;

        // Raycasting takes as parameters a Vector3 which is the point of origin, another Vector3 which gives the direction, and finally a float for the maximum distance of the raycast
        var originRay = transform.position + raycastDirection * _raycastingDistance - new Vector3(0f, 0.25f, 0f);
        RaycastHit2D hitForvard = Physics2D.Raycast(originRay, raycastDirection, 0.075f, WhatIsGround);

        // if we hit something, check its tag and act accordingly
        if (hitForvard.collider != null)
        {
            if (hitForvard.transform.tag == "Terrain")
            {
                Flip();
            }
        }
    }
    private void CheckForHoles()
    {
        Vector3 raycastDirection = (isFacingRight) ? Vector3.right : Vector3.left;

        // Raycasting takes as parameters a Vector3 which is the point of origin, another Vector3 which gives the direction, and finally a float for the maximum distance of the raycast
        var originRay = transform.position + raycastDirection * _raycastingDistance - new Vector3(0f, 0.25f, 0f);
        RaycastHit2D hitBelow = Physics2D.Raycast(originRay, Vector3.down, 1f, WhatIsGround);

        if (hitBelow.collider == null)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
    public override void Atack()
    {
        throw new System.NotImplementedException();
    }

    public override void Death()
    {
        throw new System.NotImplementedException();
    }

}
