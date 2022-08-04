using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    CharacterConroller target;
    Rigidbody2D _rigidbody2D;
    Vector2 moveDirection;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<CharacterConroller>();
        moveDirection = (target.transform.position - transform.position).normalized * speed;
        _rigidbody2D.velocity = new Vector2 (moveDirection.x, moveDirection.y + 5);
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }
}
