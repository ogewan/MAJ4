using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * Entry point for Player scripts
 * </summary>
 */
public class Player : MonoBehaviour
{
    public PlayerMovement pm;
    public PlayerShooting ps;
    public defensebarrier db;
    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        ps = GetComponent<PlayerShooting>();
        db = GetComponent<defensebarrier>();
        RegisterPlayer();
    }
    private void RegisterPlayer()
    {
        GameManager.instance.player = this;
    }
}