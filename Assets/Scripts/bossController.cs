using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public Transform player;
    public bool trackPlayer = false;
    public Vector2 timeToRam;
    public float rammingTime = 5f;
    public float ramSpeed = 5f;
    public int health = 5;
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
            else if (trackPlayer) transform.up = player.position - transform.position;
            transform.Translate(Vector3.up * ramSpeed * Time.deltaTime);
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
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bullet")
        {
            health--;
            Debug.Log("Boss damaged");
        }
    }
}