using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerPosition;
    public Transform spawnPoint;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        transform.position = transform.position + new Vector3(moveX * speed * Time.deltaTime, moveY * speed * Time.deltaTime, 0);
    }
}
