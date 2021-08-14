using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovement : MonoBehaviour
{
    public Transform spawnPoint;
    public float speed = 10f;

    public void Movement(Vector3 move)
    {
        transform.position += move * speed * Time.deltaTime;
    }
    // Start is called before the first frame update
    private void Start()
    {
        transform.position = spawnPoint.position;
    }
}