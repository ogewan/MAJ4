using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
                Canvas canvas = FindObjectOfType<Canvas>();
                EventSystem es = FindObjectOfType<EventSystem>();
                if (canvas == null)
                {
                    canvas = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster)).GetComponent<Canvas>();
                    var scaler = canvas.gameObject.GetComponent<CanvasScaler>();
                    scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                }
                if (es == null)
                {
                    es = new GameObject("Canvas", typeof(EventSystem), typeof(StandaloneInputModule)).GetComponent<EventSystem>();
                }
                // This loads a prefab to create this singleton (This allows settings to be added in the editor via prefab)
                GameObject consoleObject = Instantiate(Resources.Load<GameObject>("Console Commands"), canvas.transform);
                _instance = consoleObject.GetComponent<Console>();
            }
            else _instance = over;
        }
        return _instance;
    }
    public void SelectObject(GameObject select)
    {
        if (ccActive)
        {
            selected = select;
            selectionText.text = selected.name;
        }
    }
    public string Process(string input)
    {
        switch (input)
        {
            case "Help":
                {
                    break;
                }
        }
        return input;
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