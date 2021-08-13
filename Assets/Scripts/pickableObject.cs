using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class pickableObject : MonoBehaviour
{
    public int cases;
    private GameObject player;
    private bool OpenDoor;
    private GameObject healthSystem;
    // public GameObject BarrierDefense;

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
            if (cases == 0)
            {
                instance.player.ps.CanOpenTheDoor = true;
            }
            else if (cases == 1)
            {
                instance.ChangeHealth(1);
            }
            else if (cases == 2)
            {
                instance.player.db.isActivated = true;
            }
            Destroy(gameObject);
        }
    }
}