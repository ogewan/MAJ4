using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float speedEnemy;
    private float TimeElapsedEnemy;
    public float TimeToDestroyEnemy;

    void Update()
    {
       transform.Translate(Vector3.up * speedEnemy * Time.deltaTime);

        if(TimeElapsedEnemy > TimeToDestroyEnemy)
        {
            Destroy(gameObject);
           
        }
        else
        {
            TimeElapsedEnemy += Time.deltaTime;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "player")
        {
            Destroy(gameObject);
            Debug.Log("Player took DAMAGE");
        }
    }
}
