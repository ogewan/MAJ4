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
    public interface IComponentData
    {
        void Apply(Editable data);
        string Serialize(Editable data);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Console.instance.SelectObject(this);
    }
    public void Reset()
    {
        JSONToComponents(original);
        edited = false;
    }
    public string Dump()
    {
        return ComponentsToJSON();
    }
    public string Load(string data)
    {
        return JSONToComponents(data);
    }
    [System.Serializable]
    public class TransformData : IComponentData
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;

        public void Apply(Editable data)
        {
            data.transform.position = position;
            data.transform.eulerAngles = rotation;
            data.transform.localScale = scale;
        }

        public string Serialize(Editable data)
        {
            position = data.transform.position;
            rotation = data.transform.eulerAngles;
            scale = data.transform.localScale;
            return JsonUtility.ToJson(this, true);
        }
    }
    [SerializeField]
    [TextArea]
    private string original;
    private Dictionary<string, Type> dataType = new Dictionary<string, Type> {
        { "TransformData", typeof(TransformData) }
    };
    private bool JsonToComponentDataBackup(string jsonType, string jsonData)
    {
        // dynType = Type.GetType($"{jsonType}Data", false);
        dataType.TryGetValue(jsonType, out Type dynType);
        if (dynType != null)
        {
            IComponentData dataJson = (IComponentData)JsonUtility.FromJson(jsonData, dynType);
            dataJson.Apply(this);
            return true;
        }
        return false;
    }
    private string ComponentDataToJsonBackup(string strType, Component comp)
    {
        //var dynType = Type.GetType($"{strType}Data", false);
        dataType.TryGetValue($"{strType}Data", out Type dynType);
        if (dynType != null)
        {
            Activator.CreateInstance(dynType);
            IComponentData dataJson = (IComponentData)Activator.CreateInstance(dynType);
            return $"[{strType}Data]\n{dataJson.Serialize(this)}";
        }
        return "";
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
                var backup = ComponentDataToJsonBackup(comp.GetType().Name, comp);
                if (backup.Length > 0) data += $"{(data.Length == 0 ? "" : "\n")}{backup}";
                else Debug.Log($"Could not serialize {comp.GetType().Name}");
            }
        }
        if (original.Length == 0) original = data;
        return data;
    }
    private string JSONToComponents(string data)
    {
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
                        if (!JsonToComponentDataBackup(resultType, resultJson)) error += $"Object of Type '{resultType}' could not be parsed: {resultJson}";
                        //+ $"\n{ex.ToString()}";
                    }
                    resultJson = "";
                }
            }
        }
        return error;
    }
}