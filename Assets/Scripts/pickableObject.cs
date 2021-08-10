using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableObject : MonoBehaviour
{
    
    public GameObject player;
    private bool OpenDoor;
    

    
   private void OnTriggerEnter2D(Collider2D other)
   {
       if(other.tag == "player")
       {
           player.GetComponent<PlayerShooting>().CanOpenTheDoor = true;
           Destroy(gameObject);
       }
   }
}
