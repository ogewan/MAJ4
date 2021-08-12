using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [Header("Console Permissions")]
    [Tooltip("Allows Console Commands")]
    public bool reset;
    [Tooltip("Allows Console Commands")]
    public bool enable, disable, read, edit = false;
    [Header("Change Status")]
    [Tooltip("Has this GameObject been edited")]
    public bool edited = false;
    [Header("Data")]
    [Tooltip("Components listed here will be accessible via the Console. \nDrag component from this GameObject here. Component must have [System.Serializable].")]
    public Component[] components;
    public void OnPointerClick(PointerEventData eventData)
    {
        Console.instance.SelectObject(this);
    }
    public void Reset()
    {
        edited = false;
    }
    public string Dump()
    {
        return ComponentsToJSON();
    }
    public string Load(string data)
    {
        Debug.Log(data);
        var lines = data.Split('\n');
        string resultType = "";
        string resultJson = "";
        bool startJson = false;
        string error = "";
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (line.Length == 0) continue;
            else if (line[0] == '[' && line.Length > 2)
            {
                resultType = line.Substring(1, line.Length - 2);
            }
            else if (line[0] == '{')
            {
                startJson = true;
                resultJson += line;
            }
            else if (startJson)
            {
                resultJson += line;
                if (line[0] == '}')
                {
                    startJson = false;
                    try
                    {
                        JsonUtility.FromJsonOverwrite(resultJson, GetComponent(Type.GetType(resultType, false)));
                        edited = true;
                    }
                    catch (Exception ex)
                    {
                        error += $"Object of Type '{resultType}' could not be parsed: {resultJson}";
                        //+ $"\n{ex.ToString()}";
                    }
                    resultJson = "";
                }
            }
        }
        return error;
    }
    [System.Serializable]
    public class TransformData
    {
        public Vector3 location;
        public string name;
    }
    private string ComponentsToJSON()
    {
        string data = "";
        for (int i = 0; i < components.Length; i++)
        {
            var comp = components[i];
            try
            {
                data += $"{(data.Length == 0 ? "" : "\n")}[{comp.GetType().Name}]\n{JsonUtility.ToJson(comp, true)}";
            }
            catch
            {
                Debug.Log($"Could not serialize {comp.GetType().Name}");
            }
        }
        return data;
    }
}