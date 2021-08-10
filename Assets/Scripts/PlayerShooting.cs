using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform ShootPosition;
    public bool CanOpenTheDoor = false;
    Vector3 objectPos;
    Vector3 mousePos;
    float angle;


    void Update()
    {
        mousePos = Input.mousePosition;
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        angle = Mathf.Atan2(mousePos.y - objectPos.y, mousePos.x - objectPos.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (Input.GetMouseButtonDown(0)) // 0 for left click
            Instantiate(rb, ShootPosition.position, ShootPosition.rotation);
    }
}
