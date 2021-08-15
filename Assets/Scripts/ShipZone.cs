using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class ShipZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (TagCheck(collision, "player")) instance.weightless = false;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagCheck(collision, "player")) instance.weightless = false;
    }*/
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (TagCheck(collision, "player")) instance.weightless = true;
    }
}