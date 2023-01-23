using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Shuriken : MonoBehaviour
{

    public PlayerData PlayerDataRef;
    public GameObject diePEffect;
    public GameObject cage;
    private int damage;

    private Cage _cage;

    public AudioSource hitGround;
    
    // Start is called before the first frame update

    private void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;


        if (collisionGameObject.name != "Player")
        {
            if (collisionGameObject.CompareTag("Ground"))
            {
                hitGround.Play();
            }

            Die();
        }
        
        
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.5f, PlayerDataRef.enemyLayers);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.isTrigger == false)
            {
                enemy.GetComponent<Enemy>().tookDamage = false;
                enemy.GetComponent<Enemy>().TakeDamage(PlayerDataRef.bulletDamage);
            }
               
        }
       
        
    }

    void Die()
    {
        if (diePEffect != null)
        {
            GameObject particle = Instantiate(diePEffect, transform.position, Quaternion.identity);
            Destroy(particle, 1);
                
        }
            
        Destroy(gameObject);
    }

  
}
