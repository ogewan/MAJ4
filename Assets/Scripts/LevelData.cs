using System.Collections;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [Tooltip("Level ID")]
    [Min(0)]
    public int id = 0;
    [Tooltip("This message is displayed upon first opening the console")]
    [TextArea]
    public string note;// = $"";
    [Tooltip("These commands are executed in the console on first load")]
    public string[] precommands;
    [Tooltip("Audio of BGM to play")]
    public int audioTrack = 0;
    public Editable[] goReference = new Editable[] { };

    private void Start()
    {
        RegisterLevel();
    }
    private void RegisterLevel()
    {
        GameManager.instance.level = this;
    }
}