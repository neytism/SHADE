using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cage : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    
    public bool tookDamage;

    private PlayerData _playerData;
    private PlayerMovement _playerMovement;

    public Image HPBar;
    public Image HPContainter;

    


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
        currentHealth = maxHealth;
        HPBar.gameObject.SetActive(false);
        HPContainter.gameObject.SetActive(false);
        _playerData = GameObject.FindObjectOfType<PlayerData>();
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HPBarUpdate();

        if (currentHealth <= 0)
        {
            Die();
            
        }
        
        
    }

    void Die()
    {
        Destroy(gameObject,0.2f);
    }

  
    private void HPBarUpdate()
    {
        HPBar.gameObject.SetActive(true);
        HPContainter.gameObject.SetActive(true);
        HPBar.fillAmount = currentHealth / maxHealth;
    }
   
}
