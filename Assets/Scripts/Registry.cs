using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * Registry.cs
 * This is a silly class that stores all globally accessed prefabs.
 * Normally, prefabs would be stored in the Resouce folder,
 * but this allows Prefabs to be accessible from Resource while being in the prefab folder.
 * </summary>
 */
public class Registry : MonoBehaviour
{
    public GenericDictionary<string, GameObject> prefabs;
    public static Registry instance => Instance();
    public static Registry Instance(Registry over = null)
    {
        // Create the singleton if it doesn't exist, otherwise return the singleton
        if (_instance == null)
        {
            if (over == null)
            {
                // This loads a prefab to create this singleton (This allows settings to be added in the editor via prefab)
                GameObject registry = Instantiate(Resources.Load<GameObject>("Registry"));
                _instance = registry.GetComponent<Registry>();
            }
            else _instance = over;
        }
        return _instance;
    }
    private static Registry _instance;
    private void Awake()
    {
        if (Instance(this) != this) Destroy(gameObject);
        else DontDestroyOnLoad(this);
    }
}