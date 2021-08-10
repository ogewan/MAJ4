using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform spawnPoint;
    public float speed = 10f;

    Vector2 lookDirection;
    float lookAngle;

    
    void Update()
    {
        lookDirection = Camera.main.WorldToScreenPoint(Input.mousePosition);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, lookAngle);

        
    }



    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (moveX == 1)
            transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
        if (moveX == -1)
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
        if (moveY == 1)
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        if (moveY == -1)
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);

        transform.position = transform.position + new Vector3(moveX * speed * Time.deltaTime, moveY * speed * Time.deltaTime, 0);
    }
}
