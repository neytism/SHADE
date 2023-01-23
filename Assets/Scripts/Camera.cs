using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    private Vector3 tempPos;
    public float addY;
    public Vector3 minVal, maxVal;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (!target)
        {
            return;
        }
        
        tempPos = transform.position;
        tempPos.x = target.position.x;
        tempPos.y = target.position.y + addY;
        
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(tempPos.x,minVal.x,maxVal.x),
            Mathf.Clamp(tempPos.y,minVal.y,maxVal.y),
            Mathf.Clamp(tempPos.z,minVal.z,maxVal.z)
            
        );
        
        
        
        

        Vector3 smoothedPos = Vector3.Lerp(transform.position, boundPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPos;
    }
}