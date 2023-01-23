using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float MovementSpeed;
    private bool isLookingRight;
    private float dirX;
    

    private SpriteRenderer enemySprite;
    private Rigidbody2D _rigidbody2D;
    
    private void Awake()
    {
        dirX = -1f;
        enemySprite = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    

    private void Update()
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

 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("InvisiWall"))
        {
            dirX *= -1f;
        }

    }
    
   
}