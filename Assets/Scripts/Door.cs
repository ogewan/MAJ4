using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SerializeField]
public class Door : MonoBehaviour
{
    public bool locked = true;
    public GameObject[] enemies = new GameObject[] { };
    public string[] keys = new string[] { };
    public GameObject doorGroup;
    public bool ToggleLock()
    {
        return SetLock(!locked);
    }
    public bool SetLock(bool status)
    {
        locked = status;
        if (doorGroup != null) doorGroup.SetActive(status);
        return status;
    }

    public bool LockConditions()
    {
        bool noKey = (enemies.Length == 0 && keys.Length == 0);
        return noKey || EnemyIsAlive() || MissingKey();
    }
    public void UnlockOnCheck()
    {
        SetLock(LockConditions());
    }
    public bool EnemyIsAlive()
    {
        //there exists an enemy that is active
        return Array.Exists(enemies, e => e.activeInHierarchy);
    }
    public bool MissingKey()
    {
        var gmkeys = GameManager.instance.keys;
        for (int i = 0; i < keys.Length; i++)
        {
            //game manager does not contain this key
            if (!gmkeys.Contains(keys[i]))
            {
                return true;
            }
        }
        return false;
    }
    private void Start()
    {
        RegisterDoor();
    }

    private void RegisterDoor()
    {
        GameManager.instance.doors.Add(this);
    }

    // Update is called once per frame
    private void Update()
    {
        if (locked != doorGroup.activeInHierarchy)
        {
            SetLock(locked);
        }
    }
}