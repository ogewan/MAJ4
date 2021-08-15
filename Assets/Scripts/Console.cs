using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public bool ccActive;
    public Editable selected;
    public List<Editable> disabled;
    public string overrridePassword = "23513892331597";
    public bool enableMode;
    public bool reloadMode;
    public bool selectMode;
    public bool loadMode;
    public bool passwordMode;
    public int levelToLoad;
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
    public string note = "";
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
                    canvas = Instantiate(Registry.instance.prefabs["BaseCanvas"]).GetComponent<Canvas>();
                }
                else if (es == null)
                {
                    es = new GameObject("Canvas", typeof(EventSystem), typeof(StandaloneInputModule)).GetComponent<EventSystem>();
                    es.transform.parent = canvas.transform;
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
        if (ccActive && select != null)
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
        string redacted = "<color=red>[redacted]</color>";

        if (enableMode)
        {
            enableMode = false;
            if (!int.TryParse(input, out int id))
            {
                response += $"{input} is not a valid number.";
            }
            else if (id >= disabled.Count || id < 0)
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
        else if (reloadMode)
        {
            reloadMode = false;
            switch (input)
            {
                case "y":
                case "yes":
                    ControlConsole(false);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                case "n":
                case "no":
                    break;
                default:
                    response += $"'{input}' is not valid input.";
                    break;
            }
        }
        else if (selectMode)
        {
            selectMode = false;
            response = "";
            var lgos = GameManager.instance.level.goReference;
            if (int.TryParse(input, out int id) && id < lgos.Count && id >= 0)
            {
                SelectObject(lgos[id]);
            }
            else
            {
                Unselect();
            }
        }
        else if (loadMode)
        {
            loadMode = false;
            if (int.TryParse(input, out int id))
            {
                levelToLoad = id;
                passwordMode = true;
                response += $"Please enter the password for Level {levelToLoad}:";
            }
            else
            {
                response += $"'{input}' is not valid input.";
            }
        }
        else if (passwordMode)
        {
            passwordMode = false;
            response = "";
            string password = GameManager.instance.GetLevel(levelToLoad);

            if (input == password)
            {
                string levelName = GameManager.instance.LoadLevel(levelToLoad);
                if (levelName.Length != 0)
                {
                    SceneManager.LoadScene(levelName);
                }
                else
                {
                    // This should never happen unless, the level name is not added to the id
                    Debug.LogError("the level name is not added to the id");
                    response += $"Fatal Error: Cannot find Level {levelToLoad}";
                }
            }
            else if (input == overrridePassword)
            {
                response += $"Loading Level {levelToLoad}...";
            }
            else
            {
                response += $"Invalid password.";
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
                        $"keys: List registered keys.\n" +
                        $"edit: Edit a GameObject's parameters.\n" +
                        $"reload: Reset a level to its start.\n" +
                        $"load: Load a level by its ID.\n" +
                        $"note: Level notes.\n" +
                        $"keys: List keys.";
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
                case "i":
                case "edit":
                    if (selected == null) response += unselected;
                    else if (!selected.edit && !selected.read) response += unsupported;
                    else
                    {
                        response += $"Opening {selected.name}...";
                        if (GameManager.instance.consoleLoaded)
                        {
                            commandWindow.SetActive(false);
                            editorWindow.SetActive(true);
                            editorSelectionText.text = $"{(!selected.edit ? "[readonly] " : "")}{selected.name}.gbj";
                            editor.readOnly = !selected.edit;
                            editor.text = selected.Dump();
                        }
                        else response += (!selected.edit) ? $"\nClosing {selected.name}..." : $"\nSaving {selected.name}...";
                    }
                    break;
                case "o":
                case "reload":
                    response += $"Reload this level? (y/n)";
                    if (GameManager.instance.consoleLoaded)
                    {
                        reloadMode = true;
                    }
                    else response += "\n Reloaded level";
                    break;
                case "n":
                case "note":
                    response += note;
                    break;
                case "l":
                case "list":
                    response += (disabled.Count == 0) ? "There are no disabled GameObjects." : ListDisabled();
                    break;
                case "select":
                    // SECRET COMMAND
                    response = redacted;
                    selectMode = true;
                    break;
                case "selectlist":
                    // SECRET COMMAND
                    response = redacted;
                    response += ListGameObjects();
                    break;
                case "a":
                case "load":
                    response += $"Enter ID to load:";
                    loadMode = true;
                    break;
                case "k":
                case "keys":
                    response += (GameManager.instance.keys.Count == 0) ? "There are no keys registered." : $"{ListKeys()}";
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
        ControlConsole(!ccActive);
    }
    public void ControlConsole(bool status)
    {
        ccActive = status;
        GameManager.instance.inConsole = status;
        consoleBackground.SetActive(ccActive);
        commandWindow.SetActive(ccActive);
        editorWindow.SetActive(false);
        AudioManager.instance.SetDistortion(status);
        if (!GameManager.instance.consoleLoaded) FirstLoad();
    }
    public void FirstLoad()
    {
        LevelData data = GameManager.instance.level;
        if (data != null)
        {
            note = data.note;
            for (int i = 0; i < data.precommands.Length; i++)
            {
                cctrl.AddToChatOutput(data.precommands[i]);
            }
            GameManager.instance.consoleLoaded = true;
        }
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
    [SerializeField]
    private CommandController cctrl;
    private string ListDisabled()
    {
        string list = "";
        for (int i = 0; i < disabled.Count; i++)
        {
            list += $"[{i}]: {disabled[i].name}\n";
        }
        return list;
    }
    private string ListGameObjects()
    {
        var levelGobjs = GameManager.instance.level.goReference;
        string list = "";
        for (int i = 0; i < levelGobjs.Count; i++)
        {
            list += $"[{i}]: {levelGobjs[i].name}\n";
        }
        return list;
    }
    private string ListKeys()
    {
        var keys = GameManager.instance.keys;
        string list = "";
        foreach (var key in keys)
        {
            list += $"{key}\n";
        }
        return list;
    }
    private void Awake()
    {
        if (Instance(this) != this) Destroy(gameObject);
        //else DontDestroyOnLoad(this);
    }
    private void Update()
    {
    }
}