using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * <summary>
 * Editable.cs
 * This class is used as the entry point for the hacking of gameObjects.
 * Console uses this class to select a gameObject via click.
 * </summary>
 */
public class Editable : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Console.instance.SelectObject(gameObject);
    }
}