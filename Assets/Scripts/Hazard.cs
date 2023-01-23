using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float Damage;
    private PlayerData _playerData;
    private PlayerMovement _playerMovement;
    
    public bool IsStayTrigger;
    
    private void Awake()
    {
        _playerData = GameObject.FindObjectOfType<PlayerData>();
        _playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
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
            StartCoroutine("DisableScript");
        }
    }
    
    IEnumerator DisableScript ()
    {
        this.enabled = false;
 
        yield return new WaitForSeconds(3f);
 
        this.enabled = true;
    }
    
    

    private void OnTriggerExit2D(Collider2D other)
    {
        //throw new NotImplementedException();
    }
}
