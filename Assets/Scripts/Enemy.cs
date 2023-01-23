using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public float scoreValue;
    private Animator _animator;

    public WeightedRandomList<GameObject> RandomItem;
    
    public bool tookDamage;

    public float Damage;
    private PlayerData _playerData;
    private PlayerMovement _playerMovement;

    public Image HPBar;
    public Image HPContainter;

    public bool IsStayTrigger;
    public bool isDead;
    public bool alreadyDropped;

    public AudioSource EnemyHitSound;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Awake()
    {
        alreadyDropped = false;
        currentHealth = maxHealth;
        HPBar.gameObject.SetActive(false);
        HPContainter.gameObject.SetActive(false);
        _playerData = GameObject.FindObjectOfType<PlayerData>();
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        isDead = false;
        
        
    }

    public void TakeDamage(int damage)
    {
        EnemyHitSound.Play();
        
        if (!tookDamage)
        {
            _animator.SetTrigger("isHurt");
            tookDamage = true;
            currentHealth -= damage;
            HPBarUpdate();
        }

        if (currentHealth <= 0)
        {
            PlayerSavedValues.Instance.AddScore(scoreValue);
            scoreValue = 0;
            Die();
            
        }
        
        
    }

    void Die()
    {
        if (this.gameObject.name != "Cage")
        {
            if (!alreadyDropped)
            {
                DropItem();
                alreadyDropped = true;
            }
            _animator.SetTrigger("isDead");
        }
        
        Destroy(gameObject,0.2f);
        isDead = true;
    }

  

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag.Equals("Player"))
        {
            _playerMovement.KBCounter = _playerMovement.KBTotalTime;
            if (col.transform.position.x <= transform.position.x)
            {
                _playerMovement.KBRight = true;
            }

            if (col.transform.position.x >= transform.position.x)
            {
                _playerMovement.KBRight = false;
            }

            _playerData.ReduceHealth(Damage);
        }
        

    }
    private void HPBarUpdate()
    {
        HPBar.gameObject.SetActive(true);
        HPContainter.gameObject.SetActive(true);
        HPBar.fillAmount = currentHealth / maxHealth;
    }

    public void DropItem()
    {
        Vector3 position = transform.position;

        GameObject item = RandomItem.GetRandom();
        
        item = Instantiate(item.gameObject, position + new Vector3(0f,0.5f,0f), Quaternion.identity);
        
        
        

    }
   
}
