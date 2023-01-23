using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public List<GameObject> listOfOpponents = new List<GameObject>();
 
    void Start()
    {
    }

    private void Update()
    {
        for(var i = listOfOpponents.Count - 1; i > -1; i--)
        {
            if (listOfOpponents[i] == null)
                listOfOpponents.RemoveAt(i);
        }
        if (listOfOpponents.Count <= 0)
        {
            Destroy(gameObject);
        }
    }
}
