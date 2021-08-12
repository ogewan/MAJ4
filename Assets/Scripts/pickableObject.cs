using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableObject : MonoBehaviour
{
    
    private GameObject player;
    private bool OpenDoor;
    public int cases;
    private GameObject healthSystem;
    // public GameObject BarrierDefense;
    
    
    void Start() 
    {
     healthSystem = GameObject.FindGameObjectWithTag("hearts"); 
     player =   GameObject.FindGameObjectWithTag("player");
    //  BarrierDefense =   GameObject.FindGameObjectWithTag("defensebarriers");
    }
    
   private void OnTriggerEnter2D(Collider2D other)
   {
       if(other.tag == "player")
       {
           
           
           if(cases == 0)
           {
               player.GetComponent<PlayerShooting>().CanOpenTheDoor = true;
           }
           else if(cases == 1)
           {
               healthSystem.GetComponent<HealthSystem>().AddHealth();
           }
           else if(cases == 2)
           {
               player.GetComponent<defensebarrier>().isActivated = true;
           }
           Destroy(gameObject);
       }
   }
}
