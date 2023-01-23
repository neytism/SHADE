using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    private PlayerData _playerData;
    
    private void Awake()
    {
        _playerData = GameObject.FindObjectOfType<PlayerData>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerSavedValues.Instance.AddCoin(1);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        //throw new NotImplementedException();
    }
}