using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyV2Spawner : MonoBehaviour
{
    public float Interval;
    private float initialIntervalValue;

    public GameObject Enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        initialIntervalValue = Interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Interval > 0)
        {
            Interval -= Time.deltaTime;
        }
        else
        {
            GameObject enemyClone = Instantiate(Enemy,new Vector3(transform.position.x,transform.position.y,0),quaternion.identity);
            enemyClone.name = "EnemyV2";
            Interval = initialIntervalValue;
        }
    }
}