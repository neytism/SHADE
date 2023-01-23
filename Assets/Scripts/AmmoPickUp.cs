using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    private PlayerData _playerData;
    
    private void Awake()
    {
        _playerData = GameObject.FindObjectOfType<PlayerData>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_playerData._currentAmmo < _playerData.MaxAmmo)
        {
            if (other.gameObject.tag.Equals("Player")) 
            {
                _playerData.AddAmmo(10);
            }
            Destroy(gameObject);
        }
        
    }
}