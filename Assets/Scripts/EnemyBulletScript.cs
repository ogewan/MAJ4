using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float speedEnemy;
    private float TimeElapsedEnemy;
    public float TimeToDestroyEnemy;
    private GameObject HeartsLL;

    void Start() 
    {
      HeartsLL = GameObject.FindGameObjectWithTag("hearts");  
    }
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
            // Hearts.GetComponent<HealthSystem>().index -= 1;
            // Destroy(Hearts.GetComponent<HealthSystem>().hearts[Hearts.GetComponent<HealthSystem>().index]);
            HeartsLL.GetComponent<HealthSystem>().TakeDamage();
            Destroy(gameObject);
            Debug.Log("Player took DAMAGE");
            
        } 
       else if(other.tag == "bulletPlayer")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
       else if (other.tag == "wall")
        {
            Destroy(gameObject);
        }
        else if(other.tag == "defensebarriers")
        {
            Destroy(gameObject);
        }
    }
}
