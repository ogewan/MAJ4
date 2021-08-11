using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform ShootPoint;
    private float countdown;
    public float TimeToShoot;
    public Transform player;


    void Update()
    {
        Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);

        if(TimeToShoot < countdown)
        {
           Instantiate(rb, ShootPoint.position, ShootPoint.rotation);
           countdown = 0f;
        }
        else
        {
            countdown += Time.deltaTime;
        }
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.tag == "player")
    //     {
    //         Debug.Log("Player Took DAMAGE");
    //     }
    // }
}
