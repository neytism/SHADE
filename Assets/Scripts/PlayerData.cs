using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class PlayerData : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _sprite;
    
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    public int bulletDamage = 10;

    public float MovementSpeed;
    public float JumpForce;
    public float StompForce;

    public int JumpCount;
    public int MaxJumpCount = 2;
    
    public Image HPBar;

    //player stats
    public float MaxHealth;
    public float _currentHealth;
    public int MaxAmmo;
    public int _currentAmmo;
    public float Score;
    public TextMeshProUGUI PlayerAmmo;
    public TextMeshProUGUI PlayerCoin;
    public TextMeshProUGUI PlayerTime;
    public TextMeshProUGUI PlayerScore;
    public float CoinCount;
    
    public GameObject gameOverScreen;

    public float TimeTaken = 0;
    private float SecondTimer = 0f;
    
    public static PlayerSavedValues Instance;
    public AudioSource coinSound;
    public AudioSource hurtSound;
    public AudioSource healthSound;
    public AudioSource statueSound;
    public AudioSource ammoSound;
    public AudioSource gameOverSound;
    public AudioSource bgSound;
    

    private void Awake()
    {
        
        gameOverScreen.SetActive(false);
        CoinCount = 0;
        _currentHealth = 1;
        _currentAmmo = 0;
        PlayerAmmo.text = _currentAmmo.ToString();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        PlayerPrefs.DeleteKey("PlayerScore");
        HPBarUpdate();
       
    }
    

    private void Start()
    {
        TimeTaken = 0;
    }
    
    private void Update()
    {
        SecondTimer = SecondTimer + Time.deltaTime;
        if (SecondTimer >= 1f)
        {
            TimeTaken++;
            SecondTimer = SecondTimer - 1f;
            Debug.Log(TimeTaken);
            PlayerSavedValues.Instance.AddTime(TimeTaken);
        }
    }

    public void AddHealth(float value)
    {
        if (value == 1)
        {
            if (!statueSound.isPlaying)
            {
                statueSound.Play();
            }
            else
            {
                healthSound.Stop();
            }
        }
        else
        {
            if (!healthSound.isPlaying)
            {
                healthSound.Play();
            }
            else
            {
                healthSound.Stop();
            }
        }
       
        if (_currentHealth < MaxHealth)
        {
            _currentHealth += value;
            //_currentHealth = _currentHealth + value;
        }
        HPBarUpdate();
    }

    public void AddAmmo(int value)
    {
        ammoSound.Play();
        if (_currentAmmo < MaxAmmo)
        {
            _currentAmmo += value;
            updatePlayerAmmo();
        }
    }

    public void AddCoin(float value)
    {
        coinSound.Play();
        CoinCount += value;
        updatePlayerCoin();
    }

    public void AddScore(float value)
    {
        Score += value;
        updatePlayerScore();
    }
    public void AddTime(float value)
    {
        TimeTaken = value;
        updatePlayerTime();
    }

    public void ReduceHealth(float value)
    {
        
        hurtSound.Play();
        
        if (_currentHealth > 0)
        {
            _animator.SetTrigger("hurt");
            _currentHealth -= value;
            
            
            if (_currentHealth <= 0)
            {
                gameOverSound.Play();
                bgSound.Stop();
                GameObject.Find("Player").GetComponent<PlayerMovement>().walkingSound.Stop();
                GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
                _animator.SetTrigger("dead");
                Debug.Log("GAME OVER");
                PlayerSavedValues.Instance.CalculateScoreNotFinished();
                StartCoroutine(ShowGameOverScreen()); 
                
                
                

            }
        }
        HPBarUpdate();
    }

    public void ReduceAmmo(int value)
    {
        if (_currentAmmo > 0)
        {
            _currentAmmo -= value;

            if (_currentAmmo <= 0)
            {
                Debug.Log("No ammo left");
            }
        } 
    }

    private void HPBarUpdate()
    {
        HPBar.fillAmount = _currentHealth / MaxHealth;
    }

    public void updatePlayerAmmo()
    {
        PlayerAmmo.text = _currentAmmo.ToString();
    }

    public void updatePlayerCoin()
    {
        PlayerCoin.text = CoinCount.ToString();
    }
    
    public void updatePlayerTime()
    {
        PlayerTime.text = TimeTaken.ToString();
    }
    
    public void updatePlayerScore()
    {
        PlayerScore.text = Score.ToString("000000");
        PlayerPrefs.SetFloat("PlayerScore", Score);
    }
        


 

    IEnumerator ShowGameOverScreen() {
        yield return new WaitForSeconds(1.5f);
        gameOverScreen.SetActive(true);
        
        Time.timeScale = 0f;
    }
    
}
    
