using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorCloser : MonoBehaviour
{
    public GameObject door;

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            door.SetActive(true);
            Debug.Log("Player exited room");
        } else
        {
            Debug.Log("Something exited");
        }
        if (door.activeSelf)
        {
            Debug.Log("Door closed");
        }
    }
}
