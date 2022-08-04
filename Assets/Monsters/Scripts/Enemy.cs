using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual bool IsFacingRight { get; set; }
    public virtual float Speed { get; set; }
    public virtual int Health { get; set; }

    public SpriteRenderer _spriteRenderer;
    public virtual void Move() {
        Vector3 directionTranslation = (IsFacingRight) ? transform.right : -transform.right;
        directionTranslation *= Time.deltaTime * Speed;
        transform.Translate(directionTranslation);
    }

    public virtual void Atack()
    {
        throw new System.NotImplementedException();
    }
    public virtual void TackDamage(int damage)
    {
        Health -= damage;
    }
    public virtual void Death()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Flip()
    {
        IsFacingRight = !IsFacingRight;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}
