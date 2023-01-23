using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random=UnityEngine.Random;
public class Dog : MonoBehaviour
{
    public float MovementSpeed;
    private bool isLookingRight;
    public float agroRange;
    private float distToPlayer;
    private float countdown = 1f;
    private Transform target;
    private SpriteRenderer _dogSprite;
    private Animator _animator;
    
    private Rigidbody2D _rigidbody2D;
    private float dirX;

    public Enemy cage;

    public AudioSource barkSound;

    private void Awake()
    {
        dirX = -1f;
        _dogSprite = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        cage = GameObject.Find("Cage").GetComponent<Enemy>();
    }

    private void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, target.transform.position);

        if (distToPlayer < 60)
        {
            if (!barkSound.isPlaying)
            {
                if (countdown < 0f)
                {
                    barkSound.Play();
                    countdown = Random.Range(0.8f, 5);
                }
                else
                {
                    countdown -= Time.deltaTime;
                }
            }
        }
        
        
        if (cage.isDead)
        {
            

            if ((distToPlayer < agroRange) && (distToPlayer > 2))
            {
                //chase
                ChasePlayer();
            } else if (distToPlayer < 2)
            {
                _animator.SetBool("isWalking", false);
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(dirX * MovementSpeed, _rigidbody2D.velocity.y);
                if (dirX > 0)
                {
                    _animator.SetBool("isWalking", true);
                    _dogSprite.flipX = true;
                } else if (dirX < 0)
                {
                    _animator.SetBool("isWalking", true);
                    _dogSprite.flipX = false;
            
                }else
                {
                    _animator.SetBool("isWalking", false);
                }
            }
        }
       
        
        
    
        
        
    }

    private void ChasePlayer()
    {
        if (transform.position.x < target.transform.position.x)
        {
            _rigidbody2D.velocity = new Vector2(1 * MovementSpeed, _rigidbody2D.velocity.y);
            _animator.SetBool("isWalking", true);
            _dogSprite.flipX = true;
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(-1 * MovementSpeed, _rigidbody2D.velocity.y);
            _animator.SetBool("isWalking", true);
            _dogSprite.flipX = false;
        }

    }
    


}