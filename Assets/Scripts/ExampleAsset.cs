using System.Collections;
using UnityEngine;
/**
* <summary>
* A scriptable object.
* </summary>
*/
[CreateAssetMenu(fileName = "ExampleAsset", menuName = "ScriptableObjects/ExampleAsset", order = 1)]
public class ExampleAsset : ScriptableObject
{
    // Use public fields in ScriptableObjects
    public string message;
}