using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float MovementSpeed;
    private bool isLookingRight;

    private void Update()
    {
        if (isLookingRight)
        {
            transform.Translate((Vector2.right * (MovementSpeed * Time.deltaTime)));
        }
        else
        {
            transform.Translate((Vector2.left * (MovementSpeed * Time.deltaTime)));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
            return;
        }
        else
        {
            isLookingRight = !isLookingRight;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
    

}