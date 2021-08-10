using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform StartPosition;

    
    

    
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            Instantiate(rb, StartPosition.position, StartPosition.rotation);
        }
        
        
    }
}
