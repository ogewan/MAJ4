using System.Collections;
using UnityEngine;
/**
* <summary>
* A scriptable object.
* </summary>
*/
//[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 1)]
public class LevelData_asset : ScriptableObject
{
    // Use public fields in ScriptableObjects
    [Tooltip("Level ID")]
    [Min(0)]
    public int id = 0;
    [Tooltip("This message is displayed upon first opening the console")]
    [TextArea]
    public string note;// = $"";
    [Tooltip("This editable is selected upon first opening the console")]
    public Editable preload;
}