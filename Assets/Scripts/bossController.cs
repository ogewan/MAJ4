using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public float TimeToRam;
    public Transform player;
    public float ramDistance = 10f;
    public float speed = 1f;
    public bool trackPlayer = false;
    public Vector2 timeToRam;
    public float rammingTime = 5f;
    public float ramSpeed = 5f;
    private bool isRamming;
    private float ramTimerCD;
    private float rammingTimerCD;
    private float countdown2;
    private void Start()
    {
        ramTimerCD = Random.Range(timeToRam.x, timeToRam.y);
    }
    private void Update()
    {
        if (ramTimerCD < 0)
        {
            if (!isRamming)
            {
                transform.right = player.position - transform.position;
                isRamming = true;
                rammingTimerCD = rammingTime;
            }
            else if (trackPlayer) transform.right = player.position - transform.position;
            transform.Translate(Vector3.right * ramSpeed * Time.deltaTime);
            if (rammingTimerCD < 0)
            {
                ramTimerCD = Random.Range(timeToRam.x, timeToRam.y);
                isRamming = false;
            }
            else
            {
                rammingTimerCD -= Time.deltaTime;
            }
        }
        else
        {
            ramTimerCD -= Time.deltaTime;
        }
    }

    /*private IEnumarator bossLoop()
    {
        Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir.Normalize();
        dir *= ramDistance;

        if (TimeToRam < countdown)
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
    }*/
}