using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelData : MonoBehaviour
{
    [Tooltip("Level ID")]
    [Min(0)]
    public int id = 0;
    public string nextLevelName;
    public int initialHP = 5;
    [Tooltip("This message is displayed upon first opening the console")]
    [TextArea]
    public string note;// = $"";
    [Tooltip("These commands are executed in the console on first load")]
    public string[] precommands;
    [Tooltip("Audio of BGM to play")]
    public int audioTrack = 0;
    public Editable[] goReference = new Editable[] { };
    public bool startOnLoad = true;
    public List<string> objectives;

    private void Start()
    {
        RegisterLevel();
        InitLevel();
        GameManager.instance.gameStart = startOnLoad;
    }
    private void RegisterLevel()
    {
        var gm = GameManager.instance;
        gm.level = this;
        //Level Setup
        AudioManager.instance.PlayBGM(audioTrack);
        gm.SetRank();
    }
    private void InitLevel()
    {
        var gm = GameManager.instance;
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
        //Add Level specific canvas elements
        Instantiate(Registry.instance.prefabs["Overlay"], canvas.transform);
        Instantiate(Registry.instance.prefabs["Pause"], canvas.transform);
        Instantiate(Registry.instance.prefabs["WinScreen"], canvas.transform);
        Instantiate(Registry.instance.prefabs["LoseScreen"], canvas.transform);
        //reset parameters
        gm.keys.Clear();
        gm.clock = 0;
        gm.score = 0;
        gm.playerHealth = initialHP;
        gm.maxHealth = initialHP;
        gm.special = 0;
        gm.objectives = objectives;
    }
}