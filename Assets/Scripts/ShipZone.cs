using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class ShipZone : MonoBehaviour
{
    private bool inZone;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (TagCheck(collision, "player"))
        {
            Debug.Log("Player enter zone");
            inZone = true;
        }
    }
    private void LateUpdate()
    {
        instance.weightless = !inZone;
        inZone = false;
    }
}