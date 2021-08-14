using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerShooting : MonoBehaviour
{
    public GameObject rb;
    public Transform ShootPosition;
    public bool CanOpenTheDoor = false;
    public void Aim()
    {
        mousePos = Input.mousePosition;
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        angle = Mathf.Atan2(mousePos.y - objectPos.y, mousePos.x - objectPos.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    public void Shoot()
    {
        Instantiate(rb, ShootPosition.position, ShootPosition.rotation);
    }
    private Vector3 objectPos;
    private Vector3 mousePos;
    private float angle;
}