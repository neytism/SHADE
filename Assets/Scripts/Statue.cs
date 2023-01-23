using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public float healAmount;
    private PlayerData _playerData;
    
    public bool IsStayTrigger;
    
    private void Awake()
    {
        _playerData = GameObject.FindObjectOfType<PlayerData>();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsStayTrigger)
        {
            return;
        }
        else
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                _playerData.AddHealth(healAmount);
            }
        }
    }
    
    

    private void OnTriggerExit2D(Collider2D other)
    {
        //throw new NotImplementedException();
    }
}