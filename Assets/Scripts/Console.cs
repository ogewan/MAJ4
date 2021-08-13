using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public bool ccActive;
    public Editable selected;
    public List<Editable> disabled;
    public bool enableMode;
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
    public TextMeshProUGUI editorSelectionText;

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
                GameObject consoleObject = Instantiate(Registry.instance.prefabs["ConsoleCommands"], canvas.transform);
                _instance = consoleObject.GetComponent<Console>();
            }
            else _instance = over;
        }
        return _instance;
    }
    public void SelectObject(Editable select)
    {
        if (ccActive)
        {
            selected = select;
            selectionText.text = selected.name;
        }
    }
    public void Unselect()
    {
        selected = null;
        selectionText.text = "";
    }
    public string Process(string input)
    {
        string response = $"{input}\n";
        string unselected = $"This command requires a selected GameObject. Click the GameObject to select it.";
        string unsupported = $"This command is unsupported by the selected GameObject.";

        if (enableMode)
        {
            enableMode = false;
            if (!int.TryParse(input, out int id))
            {
                response += $"{input} is not a valid number.";
            }
            else if (id >= disabled.Count)
            {
                response += $"ID {id} is out of range, please select a listed ID.";
            }
            else
            {
                Editable enabled = disabled[id];
                enabled.gameObject.SetActive(true);
                disabled.Remove(enabled);
                SelectObject(enabled);
                response += $"Enabled {selected.name}";
            }
        }
        else
        {
            switch (input)
            {
                case "h":
                case "help":
                    response += $"// Console 1.22.1 //\n" +
                        $"**For use in development only!**\n" +
                        $"help: Display console commands.\n" +
                        $"reset: Reset a GameObject to its original parameters.\n" +
                        $"enable: Activate selected GameObject.\n" +
                        $"disable: Disable selected GameObject.\n" +
                        $"list: List disabled GameObjects.\n" +
                        $"edit: Edit a GameObject's parameters.";
                    break;
                case "r":
                case "reset":
                    if (selected == null) response += unselected;
                    else if (!selected.reset) response += unsupported;
                    else
                    {
                        response += $"Reset {selected.name}";
                        selected.Reset();
                    }
                    break;
                case "e":
                case "enable":
                    if (disabled.Count == 0) response += "There are no GameObjects to enable.";
                    else
                    {
                        response += $"Select an ID to enable:\n{ListDisabled()}";
                        enableMode = true;
                    }
                    break;
                case "d":
                case "disable":
                    if (selected == null) response += unselected;
                    else if (!selected.disable) response += unsupported;
                    else
                    {
                        response += $"Disabled {selected.name}";
                        selected.gameObject.SetActive(false);
                        disabled.Add(selected);
                        Unselect();
                    }
                    break;
                case "x":
                case "edit":
                    if (selected == null) response += unselected;
                    else if (!selected.edit && !selected.read) response += unsupported;
                    else
                    {
                        response += $"Opening {selected.name}...";
                        commandWindow.SetActive(false);
                        editorWindow.SetActive(true);
                        editorSelectionText.text = $"{(!selected.edit ? "[readonly] " : "")}{selected.name}.gbj";
                        editor.readOnly = !selected.edit;
                        editor.text = selected.Dump();
                    }
                    break;
                case "l":
                case "list":
                    response += (disabled.Count == 0) ? "There are no disabled GameObjects." : ListDisabled();
                    break;
                default:
                    response += $"'{input}' is not recognized as a command.";
                    break;
            }
        }
        return response;
    }
    public void ToggleConsole()
    {
        ccActive = !ccActive;
        consoleBackground.SetActive(ccActive);
        commandWindow.SetActive(ccActive);
        editorWindow.SetActive(false);
    }
    public void EditorQuit()
    {
        //Saves Automatically
        string error = "";
        if (!editor.readOnly) error = selected.Load(editor.text);
        if (error.Length != 0) display.text += $"\n{error}";
        display.text += (editor.readOnly) ? $"\nClosing {selected.name}..." : $"\nSaving {selected.name}...";
        commandWindow.SetActive(true);
        editorWindow.SetActive(false);
    }
    private static Console _instance;
    private string ListDisabled()
    {
        string list = "";
        for (int i = 0; i < disabled.Count; i++)
        {
            list += $"[{i}]: {disabled[i].name}\n";
        }
        return list;
    }
    private void Start()
    {
        if (Instance(this) != this) Destroy(this);
        //else DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleConsole();
        }
    }
}