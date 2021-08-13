using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    private float lifetimeTimer;
    private string opposite;
    private void Start()
    {
        lifetimeTimer = lifetime;
        opposite = tag == "bullet" ? "enemyBullet" : "bullet";
    }
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (lifetimeTimer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifetimeTimer -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TagCheck(other, "player"))
        {
            instance.ChangeHealth();
            Destroy(gameObject);
            Debug.Log("Player took DAMAGE");
        }
        //Do enemy check on enemy to not need GetComponent
        /*else if (TagCheck(other, "enemy")) {
            scores.GetComponent<ScoreScript>().ChangeScore();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }*/
        else if (TagCheck(other, "wall"))
        {
            Destroy(gameObject);
        }
        else if (TagCheck(other, opposite))
        {
            Destroy(gameObject);
        }
    }
}