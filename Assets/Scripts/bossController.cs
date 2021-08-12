using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    private float countdown;
    private float countdown2;
    public float TimeToRam;
    public Transform player;
    public float ramDistance = 10f;
    public float speed = 1f;


    void Update()
    {

    }

    IEnumarator bossLoop()
    {
        Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);
        dir.Normalize();
        dir *= ramDistance;


        if(TimeToRam < countdown)
        {
            float dist = ramDistance;
            while (dist > 0)
            {
                transform.position = transform.position + dir * speed * Time.deltaTime;
            }
            countdown = 0f;
        }
        else
        {
            countdown += Time.deltaTime;
        }
    }
}
