using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime;
    public bool immortal = false;
    private float lifetimeTimer;
    private Animator animator;
    private void Start()
    {
        lifetimeTimer = lifetime;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!immortal)
        {
            Life();
        }
    }
    private void Life()
    {
        if (GameManager.instance.isRunning)
        {
            if (lifetimeTimer <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                lifetimeTimer -= Time.deltaTime;
            }
        }
    }
}