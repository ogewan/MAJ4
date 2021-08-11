using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public bool ccActive;
    public GameObject selected;
    public GameObject consoleBackground;
    public GameObject commandWindow;
    public TMP_InputField entry;
    public TextMeshProUGUI entryText;
    public TextMeshProUGUI display;
    public TextMeshProUGUI selectionText;
    public Scrollbar displaySB;
    public GameObject editorWindow;
    public TMP_InputField editor;
    public TextMeshProUGUI editorText;

    public static Console instance => Instance();
    public static Console Instance(Console over = null)
    {
        // Create the singleton if it doesn't exist, otherwise return the singleton
        if (_instance == null)
        {
            if (over == null)
            {
                // This loads a prefab to create this singleton (This allows settings to be added in the editor via prefab)
                /*GameObject example = Instantiate(Resources.Load<GameObject>("Example"));
                example.isStatic = true;
                example.name = "__example";*/
                // Create via new GameObject
                /*GameObject example = new GameObject("__example", typeof(Example))
                {
                    isStatic = true
                };*/
                //_instance = example.GetComponent<Console>();
            }
            else _instance = over;
        }
        return _instance;
    }
    public void SelectObject(GameObject select)
    {
        selected = select;
        selectionText.text = selected.name;
    }
    private static Console _instance;

    private void Start()
    {
        if (Instance(this) != this) Destroy(this);
        //else DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            ccActive = !ccActive;
            consoleBackground.SetActive(ccActive);
            commandWindow.SetActive(ccActive);
            editorWindow.SetActive(false);
        }
    }
}