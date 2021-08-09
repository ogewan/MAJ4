using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * Global Singleton pattern
 * For scripts that need to be accessed by everything else.
 * </summary>
 */
public class Example : MonoBehaviour
{
    public static Example instance => Instance();
    // Use properties to expose fields
    public float exampleValue
    {
        // Arrow form
        //get => _exampleValue; set => _exampleValue = value;
        get
        {
            return _exampleValue;
        }
        set
        {
            _exampleValue = value;
        }
    }
    public ExampleAsset asset { get => _asset; set => _asset = value; }
    public static Example Instance()
    {
        // Create the singleton if it doesn't exist, otherwise return the singleton
        if (_instance == null)
        {
            // This loads a prefab to create this singleton (This allows settings to be added in the editor via prefab)
            GameObject example = Instantiate(Resources.Load<GameObject>("Example"));
            example.isStatic = true;
            example.name = "__example";
            // Create via new GameObject
            /*GameObject example = new GameObject("__example", typeof(Example))
            {
                isStatic = true
            };*/

            _instance = example.GetComponent<Example>();
        }
        return _instance;
    }
    private static Example _instance;
    // Make fields private
    [SerializeField]
    private float _exampleValue;
    [SerializeField]
    private ExampleAsset _asset;
    private void Start()
    {
        // Destroy self if its not the first.
        if (Instance() != _instance) Destroy(this);
    }
}