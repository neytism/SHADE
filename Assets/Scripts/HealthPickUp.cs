using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    private float healAmount = 25f;
    private PlayerData _playerData;
    
    public bool IsStayTrigger;
    
    private void Awake()
    {
        _playerData = GameObject.FindObjectOfType<PlayerData>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_playerData._currentHealth < _playerData.MaxHealth)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                _playerData.AddHealth(healAmount);
            }
            Destroy(gameObject);
        }
       
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        //throw new NotImplementedException();
    }
}