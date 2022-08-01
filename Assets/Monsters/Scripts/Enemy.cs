using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual float Speed { get; set; }
    public virtual int Health { get; set; }
    public virtual void Move() { 
    
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
}
