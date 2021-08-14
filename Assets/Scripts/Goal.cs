using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Goal : MonoBehaviour
{
    public GameObject[] enemies = new GameObject[] { };

    public bool EnemyIsAlive()
    {
        //there exists an enemy that is active
        return Array.Exists(enemies, e => e.activeInHierarchy);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (TagCheck(collision, "player") && !EnemyIsAlive())
        {
            instance.GameWin();
            Destroy(gameObject);
        }
    }
}