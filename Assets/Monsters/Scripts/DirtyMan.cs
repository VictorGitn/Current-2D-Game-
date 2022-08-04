using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyMan : Enemy
{
    private BulletSpawn bulletSpawnScript;
    private PlayerDetector playerDetector;
    private TerrainChecker terrainChecker;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    [SerializeField] private float actualSpeed;
    [SerializeField] private float speed;
    public override float Speed {
        get => speed; 
        set => speed = value; 
    }
    private int health;
    public override int Health {
        get => health;
        set => health = value;
    }
    private bool isFacingRight = true;
    public override bool IsFacingRight
    {
        get => isFacingRight;
        set => isFacingRight = value;
    }

    void Start()
    {
        playerDetector = GetComponentInChildren<PlayerDetector>();
        bulletSpawnScript = GetComponentInChildren<BulletSpawn>();
        terrainChecker = GetComponentInChildren<TerrainChecker>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        actualSpeed = Speed;
    }

    void FixedUpdate()
    {
        Atack();
        Patrol();
    }

    public void Patrol()
    {
        terrainChecker.CheckForWalls();
        terrainChecker.CheckForHoles();
        Move();
    }
    public override void Move()
    {
        base.Move();
        _animator.SetFloat("Speed", Speed);
    }

    public override void Flip()
    {
        base.Flip();
        bulletSpawnScript.TurnSpawnPosition();
    }

    public override void Atack()
    {
        if (playerDetector.PlayerDetected)
        {
            if (playerDetector.PlayerStayNear)
            {
                MeleeAtack();
            }
            else
            {
                longRangeAtack();
            }
        }
        else
        {
            Speed = actualSpeed;
            _animator.SetBool("Player Is Near", false);
            _animator.SetBool("Player In Atack Zone", false);
        }
    }

    private void MeleeAtack()
    {
        TurnToPlayer();
        Speed = 0;
        _animator.SetBool("Player Is Near", playerDetector.PlayerStayNear);
    }
    private void longRangeAtack()
    {
        _animator.SetBool("Player Is Near", false);
        TurnToPlayer();
        Speed = 0;
        _animator.SetBool("Player In Atack Zone", playerDetector.PlayerDetected);
    }

    private void TurnToPlayer()
    {
        if (IsFacingRight && playerDetector.DirectionToTarget.x < 0)
        {
            Flip();
        }
        if (!IsFacingRight && playerDetector.DirectionToTarget.x > 0)
        {
            Flip();
        }
    }

    public override void Death()
    {
        throw new System.NotImplementedException();
    }
    public void SpawnBullet()
    {
        bulletSpawnScript.SpawnBullet();
    }
}
