using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovement : MonoBehaviour
{
    public Transform spawnPoint;
    public float speed = 10f;

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // if (moveX == 1)
        //     transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
        // if (moveX == -1)
        //     transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
        // if (moveY == 1)
        //     transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        // if (moveY == -1)
        //     transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);

        transform.position = transform.position + new Vector3(moveX * speed * Time.deltaTime, moveY * speed * Time.deltaTime, 0);
    }
}