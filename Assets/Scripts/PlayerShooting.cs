using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform StartPosition;
    public bool CanOpenTheDoor = false;




    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            Instantiate(rb, StartPosition.position, StartPosition.rotation);
        }
        if(CanOpenTheDoor == true)
        {
            Debug.Log("Can Open DOOR");
        }

    }
}
