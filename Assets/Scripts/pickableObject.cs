using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

[System.Serializable]
public class pickableObject : MonoBehaviour
{
    public Pickup type;
    public int value;
    // public GameObject BarrierDefense;
    public enum Pickup { health, key, shield, special }
    private GameObject player;
    private bool OpenDoor;
    private GameObject healthSystem;
    private void Start()
    {
        //healthSystem = GameObject.FindGameObjectWithTag("hearts");
        //player = GameObject.FindGameObjectWithTag("player");
        //  BarrierDefense =   GameObject.FindGameObjectWithTag("defensebarriers");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TagCheck(other, "player"))
        {
            switch (type)
            {
                case Pickup.health:
                    instance.ChangeHealth(1);
                    break;
                case Pickup.key:
                    //changing this
                    //instance.player.ps.CanOpenTheDoor = true;
                    instance.keys.Add($"{value}");
                    instance.DoorCheck();
                    break;
                case Pickup.shield:
                    instance.player.db.isActivated = true;
                    break;
                case Pickup.special:
                    instance.special++;
                    instance.ChangeScore(20);
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}