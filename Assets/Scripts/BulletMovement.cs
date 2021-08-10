using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
     public float speed;
    

    private float TimeElapsed;
    public float TimeToDestroy;

     void Start()
    {
     TimeElapsed = 0f;
        
    }
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(TimeElapsed > TimeToDestroy)
        {
            Destroy(gameObject);
           
        }
        else
        {
            TimeElapsed += Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "enemy")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
