using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    // Destroys itself if an instance of this class already exists
    private void Start()
    {
        Object[] other = FindObjectsOfType(GetType());
        if (other.Length != 1) Destroy(gameObject);
    }
}