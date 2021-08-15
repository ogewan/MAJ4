using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;

public class Switch : MonoBehaviour
{
    public bool visible = false;
    public Sprite off;
    public Sprite on;
    public string[] tags;
    public bool permanent;
    public bool accumulate;
    public int goal;
    public int accumulation = 0;
    public float accumulationSpeed;
    public float accumulationCD;
    [Tooltip("Status for doors when active")]
    public GenericDictionary<Door, bool> doorStatus = new GenericDictionary<Door, bool> { };
    [Tooltip("Status for GameObjects when active")]
    public GenericDictionary<GameObject, bool> itemAvaliable = new GenericDictionary<GameObject, bool> { };
    public bool activated;
    public TextMeshPro count;
    public bool showCount;
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        Negation();
        if (!showCount) count.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (sprite != null && visible != sprite.enabled)
        {
            sprite.enabled = visible;
        }
        if (accumulate && activated)
        {
            Accumulation();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsValid(collision))
        {
            if (!accumulate) Confirmation();
            activated = true;
            SetDisplay(activated);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsValid(collision) && !permanent)
        {
            if (!accumulate) Negation();
            activated = false;
            SetDisplay(activated);
        }
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
            if (entry.Key != null) entry.Key.SetActive(entry.Value);
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
            if (entry.Key != null) entry.Key.SetActive(!entry.Value);
        }
    }
    private void Accumulation()
    {
        if (instance.isRunning)
        {
            if (accumulation >= goal) Confirmation();
            else if (accumulationCD <= 0)
            {
                accumulationCD = accumulationSpeed;
                accumulation++;
            }
            else
            {
                accumulationCD -= Time.deltaTime;
            }
            if (showCount)
            {
                count.text = $"{accumulation}";
            }
        }
    }
    private void SetDisplay(bool display)
    {
        sprite.sprite = display ? on : off;
    }
}