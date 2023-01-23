using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData PlayerDataRef;
    public EnemyMovement _EnemyMovement;


    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KBRight;

    private bool _isRunning;
    private bool _isGrounded;
    private bool _isReallyGrounded;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _sprite;
    
    public GameObject Bullet;
    public Transform BulletSpawnPosition;
    public Transform CamTargetPosition;
    public float BulletForce;

    private Vector3 _respawnPoint;
    public GameObject fallDetector;
    
    //audio

    public AudioSource jumpSound;
    public AudioSource isGroundSound;
    public AudioSource walkingSound;
    public AudioSource swordSound;
    public AudioSource throwSound;
    public AudioSource noammoSound;
    private void Start()
    {
        StartCoroutine("DisableScript");
        _respawnPoint = transform.position;
    }

    private void Awake()
    {
       
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        PlayerJump();
        AnimatePlayer();
        PlayerAttack();
        FlipAttackPoint();
        MoveFallDetector();
    }

    private void FixedUpdate()
    {

    }

    private void PlayerMoveKeyboard()
    {
        if (KBCounter <= 0)
        {
            
            _rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * PlayerDataRef.MovementSpeed,
                _rigidbody2D.velocity.y);
        }
        else
        {
            PlayerKnockBack();
        }

    }

    private void PlayerKnockBack()
    {
        
        if (KBRight == true)
        {
            _rigidbody2D.velocity = new Vector2(-KBForce, KBForce);
        }

        if (KBRight == false)
        {
            _rigidbody2D.velocity = new Vector2(KBForce, KBForce);
        }

        KBCounter -= Time.deltaTime;
    }

    private void PlayerJump()
    {

        if (PlayerDataRef.JumpCount == 0)
        {
            _isReallyGrounded = true;
        }
        
        if (PlayerDataRef.JumpCount > PlayerDataRef.MaxJumpCount - 1)
        {
            _isGrounded = false;
           
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) ) && _isGrounded)
        {
            jumpSound.Play();
            
            _animator.SetBool("isJumping", true);
            _animator.SetTrigger("takeOff");
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, PlayerDataRef.JumpForce);
            PlayerDataRef.JumpCount += 1;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) ) && _rigidbody2D.velocity.y > 0)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * .5f);
        }
        
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ) && (!_isGrounded || _rigidbody2D.velocity.y > 0))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, PlayerDataRef.StompForce*-1);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isReallyGrounded = true;
            _isGrounded = true;
            PlayerDataRef.JumpCount = 0;
            _animator.SetBool("isJumping", false);
            isGroundSound.Play();
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            
            _isReallyGrounded = false;
          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("FallDetector"))
        {
            PlayerDataRef.ReduceHealth(10);
            if (PlayerDataRef._currentHealth > 0)
            {
                transform.position = _respawnPoint;
            }
            else 
            {
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }
                
            
        }
        
        if (collision.gameObject.CompareTag("RespawnPoint"))
        {
            _respawnPoint = collision.transform.position;

        }
    }

    private void AnimatePlayer()
    {
        if (_rigidbody2D.velocity.x < 0)
        {
           
            _animator.SetBool("isWalking", true);
            _sprite.flipX = true;
            _isRunning = true;
            
        }
        else if (_rigidbody2D.velocity.x > 0)
        {
            
            _animator.SetBool("isWalking", true);
            _sprite.flipX = false;
            _isRunning = true;
        }
        else
        {
            
            _animator.SetBool("isWalking", false);
            _isRunning = false;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            PlayerDataRef.attackPoint.localPosition = new Vector3(PlayerDataRef.attackPoint.localPosition.x * Input.GetAxisRaw("Horizontal"),PlayerDataRef.attackPoint.localPosition.x );
        }

        if (_isRunning  && _isReallyGrounded)
        {
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }
        else
        {
            walkingSound.Stop();
        }
    }

    private void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swordSound.Play();
            _animator.SetTrigger("attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(PlayerDataRef.attackPoint.position,
                PlayerDataRef.attackRange, PlayerDataRef.enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.isTrigger == false)
                {
                    enemy.GetComponent<Enemy>().tookDamage = false;
                    enemy.GetComponent<Enemy>().TakeDamage(PlayerDataRef.attackDamage);
                    
                }
               
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (PlayerDataRef._currentAmmo > 0)
            {
                throwSound.Play();
                _animator.SetTrigger("throw");
                PlayerDataRef.ReduceAmmo(1);
                GameObject bulletClone = Instantiate(Bullet,BulletSpawnPosition.position,Quaternion.identity);
                Destroy(bulletClone,3);
                if (_sprite.flipX)
                {
                    bulletClone.GetComponent<Rigidbody2D>().AddForce(-transform.right * BulletForce,ForceMode2D.Impulse);
                }
                else
                {
                    bulletClone.GetComponent<Rigidbody2D>().AddForce(transform.right * BulletForce,ForceMode2D.Impulse);
                }
                PlayerDataRef.updatePlayerAmmo();
            }
            else
            {
                noammoSound.Play();
            }
        }
    }
    
    private void FlipAttackPoint()
    {
        if (_sprite.flipX)
        {
            PlayerDataRef.attackPoint.localPosition = new Vector3(-0.25f, 0,0);
            BulletSpawnPosition.localPosition = new Vector3(-.03f, .0f, 0);
            CamTargetPosition.localPosition = new Vector3(-.5f, .0f, 0);
        }
        else
        {
            PlayerDataRef.attackPoint.localPosition = new Vector3(0.25f, 0,0);
            BulletSpawnPosition.localPosition = new Vector3(.03f, .0f, 0);
            CamTargetPosition.localPosition = new Vector3(.5f, .0f, 0);
        }
    }
    
    IEnumerator DisableScript ()
    {
        this.enabled = false;
 
        yield return new WaitForSeconds(3f);
 
        this.enabled = true;
    }

    public void MoveFallDetector()
    {
        fallDetector.transform.position = new Vector2(transform.position.x,fallDetector.transform.position.y);
    }
    
    
    
  
    }

