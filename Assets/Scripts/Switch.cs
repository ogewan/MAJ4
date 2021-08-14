using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Switch : MonoBehaviour
{
    public bool visible = false;
    public string[] tags;
    [Tooltip("Status for doors when active")]
    public GenericDictionary<Door, bool> doorStatus = new GenericDictionary<Door, bool> { };
    [Tooltip("Status for GameObjects when active")]
    public GenericDictionary<GameObject, bool> itemAvaliable = new GenericDictionary<GameObject, bool> { };
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Negation();
    }
    private void Update()
    {
        if (sprite != null && visible != sprite.enabled)
        {
            sprite.enabled = visible;
        }
    }
    /*private void OnTriggerStay2D(Collider2D collision)
    {
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsValid(collision)) Confirmation();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsValid(collision)) Negation();
    }
    private bool IsValid(Collider2D collision)
    {
        return tags.Length == 0 || Array.Exists(tags, t => TagCheck(collision, t));
    }
    private void Confirmation()
    {
        foreach (KeyValuePair<Door, bool> entry in doorStatus)
        {
            entry.Key.SetLock(entry.Value);
        }
        foreach (KeyValuePair<GameObject, bool> entry in itemAvaliable)
        {
            entry.Key.SetActive(entry.Value);
        }
    }
    private void Negation()
    {
        foreach (KeyValuePair<Door, bool> entry in doorStatus)
        {
            entry.Key.SetLock(!entry.Value);
        }
        foreach (KeyValuePair<GameObject, bool> entry in itemAvaliable)
        {
            entry.Key.SetActive(!entry.Value);
        }
    }
}