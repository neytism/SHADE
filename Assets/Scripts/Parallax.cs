using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length, height, startposX, startposY;
    public GameObject cam;
    public float parallaxEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        startposX = transform.position.x ;
        startposY = transform.position.y ;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void FixedUpdate()
    {
        float dist = (cam.transform.position.x * parallaxEffect);
        float dist2 = (cam.transform.position.y * parallaxEffect);
        
        
        transform.position = new Vector3(startposX + dist, startposY + dist2, transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
