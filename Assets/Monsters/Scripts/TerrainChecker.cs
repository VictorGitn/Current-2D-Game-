using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChecker : MonoBehaviour
{
    [SerializeField] float _raycastingDistance = 1f;
    [SerializeField] LayerMask WhatIsGround;
    [SerializeField] string terrainTag;
    [SerializeField] Enemy enemy;
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void CheckForWalls()
    {
        Vector3 raycastDirection = (enemy.IsFacingRight) ? Vector3.right : Vector3.left;
        //var originOffset = new Vector3(0, transform.localScale.y / enemy._spriteRenderer.sprite.bounds.size.y, 0);
        var originRay = transform.position + raycastDirection * _raycastingDistance;
        RaycastHit2D hitForvard = Physics2D.Raycast(originRay, raycastDirection, 0.075f, WhatIsGround);

        //Debug.DrawLine(originRay, raycastDirection, Color.green, 0.075f);
        if (hitForvard.collider != null)
        {
            if (hitForvard.transform.tag == terrainTag)
            {
                enemy.Flip();
            }
        }
    }
    public void CheckForHoles()
    {
        Vector3 raycastDirection = (enemy.IsFacingRight) ? Vector3.right : Vector3.left;
        //var originOffset = new Vector3(0, transform.localScale.y / enemy._spriteRenderer.sprite.bounds.size.y, 0);
        var originRay = transform.position + raycastDirection * _raycastingDistance;
        RaycastHit2D hitBelow = Physics2D.Raycast(originRay, Vector3.down, 1f, WhatIsGround);

        //Debug.DrawLine(originRay, Vector3.down, Color.red , 1f);
        if (hitBelow.collider == null)
        {
            enemy.Flip();
        }
    }
}
