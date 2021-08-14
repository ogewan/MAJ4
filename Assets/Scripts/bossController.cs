using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class bossController : MonoBehaviour
{
    //public Transform player;
    public bool trackPlayer = false;
    public Vector2 timeToRam = new Vector2(3, 5);
    public float rammingTime = 5f;
    public float ramSpeed = 5f;
    public int health = 5;
    public GameObject[] reward = new GameObject[] { };
    public int scoreReward = 50;
    private bool isRamming;
    private float ramTimerCD;
    private float rammingTimerCD;
    private void RammingMode()
    {
        Transform player = GameManager.instance.player.transform;
        if (!isRamming)
        {
            transform.up = player.position - transform.position;
            isRamming = true;
            rammingTimerCD = rammingTime;
        }
        else if (trackPlayer) transform.up = player.position - transform.position;

        transform.Translate(Vector3.up * ramSpeed * Time.deltaTime);

        if (rammingTimerCD <= 0)
        {
            ramTimerCD = Random.Range(timeToRam.x, timeToRam.y);
            isRamming = false;
        }
        else
        {
            rammingTimerCD -= Time.deltaTime;
        }
    }
    private void Start()
    {
        ramTimerCD = Random.Range(timeToRam.x, timeToRam.y);
    }
    private void Update()
    {
        if (instance.isRunning)
        {
            if (ramTimerCD <= 0)
            {
                RammingMode();
            }
            else
            {
                ramTimerCD -= Time.deltaTime;
            }
            if (health <= 0)
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
                instance.DoorCheck();
                instance.ChangeScore(scoreReward);
                foreach (var obj in reward)
                {
                    var mypos = transform.position;
                    mypos.x += Random.Range(-2f, 2f);
                    mypos.y += Random.Range(-2f, 2f);

                    Instantiate(obj, mypos, Quaternion.identity);
                }
                Instantiate(Registry.instance.prefabs["Explosion"], transform.position, Quaternion.identity);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TagCheck(other, "bullet"))
        {
            Destroy(other.gameObject);
            health--;
            //Debug.Log("Boss damaged");
        }
        else if (TagCheck(other, "player"))
        {
            instance.ChangeHealth();
            //Debug.Log("Player took DAMAGE");
        }
    }
}