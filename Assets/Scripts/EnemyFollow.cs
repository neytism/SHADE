using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float MovementSpeed;
    private bool isLookingRight;
    public float agroRange;
    private float distToPlayer;
    
    private Transform target;
    private SpriteRenderer enemySprite;
    
    private Rigidbody2D _rigidbody2D;
    private float dirX;

    private void Awake()
    {
        dirX = -1f;
        enemySprite = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        
        distToPlayer = Vector2.Distance(transform.position, target.transform.position);

        if (distToPlayer < agroRange)
        {
            //chase
            ChasePlayer();
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(dirX * MovementSpeed, _rigidbody2D.velocity.y);
            if (dirX > 0)
            {
                enemySprite.flipX = false;
            } else if (dirX < 0)
            {
                enemySprite.flipX = true;
            
            }
        }
        
        
        
    }

    private void ChasePlayer()
    {
        if (transform.position.x < target.transform.position.x)
        {
            _rigidbody2D.velocity = new Vector2(1 * MovementSpeed, _rigidbody2D.velocity.y);
            enemySprite.flipX = false;
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(-1 * MovementSpeed, _rigidbody2D.velocity.y);
            enemySprite.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("InvisiWall"))
        {
            dirX *= -1f;
        }

    }
}