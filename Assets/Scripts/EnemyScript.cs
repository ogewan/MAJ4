using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

[System.Serializable]
public class EnemyScript : MonoBehaviour
{
    //public Rigidbody2D rb;
    public int health = 1;
    public GameObject rb;
    public Transform ShootPoint;
    public float shootTime;
    public bool damageOnContact;
    public bool selfDamageOnContact;
    public int scoreReward = 5;
    public int crashReward = 0;
    //public GenericDictionary<GameObject, float> reward = new GenericDictionary<GameObject, float> { };
    public GameObject[] reward = new GameObject[] { };
    public void Damage(int val = 1)
    {
        health -= val;
        if (health <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            instance.DoorCheck();
            foreach (var obj in reward)
            {
                var mypos = transform.position;
                mypos.x += Random.Range(-2f, 2f);
                mypos.y += Random.Range(-2f, 2f);

                Instantiate(obj, mypos, Quaternion.identity);
            }
        }
    }
    private bool setParticleTarget = false;
    private ParticleSystem ps;
    private float shootTimeCD;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (ps && instance.player && setParticleTarget)
        {
            ps.trigger.AddCollider(instance.player.transform);
            setParticleTarget = false;
        }

        if (instance.isRunning)
        {
            Transform player = instance.player.transform;
            Vector3 dir = player.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            if (shootTimeCD <= 0)
            {
                var bullet = Instantiate(rb, ShootPoint.position, ShootPoint.rotation);
                bullet.tag = "enemyBullet";
                shootTimeCD = shootTime;
            }
            else
            {
                shootTimeCD -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Enemies can only take damage while the game is running
        if (instance.isRunning)
        {
            if (TagCheck(other, "player") && damageOnContact)
            {
                instance.ChangeHealth();
                instance.ChangeScore(crashReward);
                Debug.Log("Player took DAMAGE");
                if (selfDamageOnContact) Damage();
            }
            else if (TagCheck(other, "bullet"))
            {
                instance.ChangeScore(scoreReward);
                Destroy(other.gameObject);
                Damage();
            }
        }
    }
}