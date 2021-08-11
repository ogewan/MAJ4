using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    public List<GameObject> doors;
    public List<bool> open;
    public List<GameObject> enemies;

    void Start()
    {
        if (doors.Count != open.Count)
        {
            Debug.Log("Not every doors have a way of being enabled");
        }
    }


    void Update()
    {
        if (enemies[0] == null && enemies[1] == null)
        {
            open[0] = true;
        }
        if (enemies[2] == null && enemies[3] == null)
        {
            open[1] = true;
        }
        for (int i = 0; i < doors.Count; i++)
        {
            if (open[i])
            {
                doors[i].SetActive(false);
            } else
            {
                doors[i].SetActive(true);
            }
        }
    }
}
