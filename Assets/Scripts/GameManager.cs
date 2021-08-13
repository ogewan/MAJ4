using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * GameManager.cs
 * Handles player update and game state: paused, console, level end
 * </summary>
 */
[System.Serializable]
public class GameManager : MonoBehaviour
{
    public LevelData level;
    public int playerHealth = 5;
    public float clock;
    public int score;
    public Player player;
    public Pause pauseMenu;
    public HealthUI healthDisplay;
    public ScoreUI scoreDisplay;
    public TimerUI timerDisplay;
    public bool ccAllowed;
    public bool consoleLoaded = false;

    public bool gameStart = false;
    public bool inPause = false;
    public bool inConsole = false;
    public static GameManager instance => Instance();
    public bool isRunning => IsRunning();
    public static GameManager Instance(GameManager over = null)
    {
        // Create the singleton if it doesn't exist, otherwise return the singleton
        if (_instance == null)
        {
            if (over == null)
            {
                // This loads a prefab to create this singleton (This allows settings to be added in the editor via prefab)
                GameObject registry = Instantiate(Registry.instance.prefabs["GameManager"]);
                _instance = registry.GetComponent<GameManager>();
            }
            else _instance = over;
        }
        return _instance;
    }
    public static bool TagCheck(Collision2D col, string tag)
    {
        return col.gameObject.CompareTag(tag);
    }
    public static bool TagCheck(Collider2D col, string tag)
    {
        return col.gameObject.CompareTag(tag);
    }
    public bool IsRunning()
    {
        return !(inPause || inConsole) && gameStart;
    }
    public void ChangeHealth(int val = -1)
    {
        playerHealth += val;
        healthDisplay.ShowHealth(playerHealth);
    }
    public void ChangeScore(int val = 5)
    {
        score += val;
        scoreDisplay.UpdateScore();
    }

    public string GetLevel(int level)
    {
        return (level >= 0 && level < levelPasswordList.Count) ? levelPasswordList[level] : "";
    }
    public void SetPassList(List<string> passList)
    {
        levelPasswordList = passList;
    }
    public void SetNameList(List<string> nameList)
    {
        levelNameList = nameList;
    }
    public string LoadLevel(int level)
    {
        return (level >= 0 && level < levelNameList.Count) ? levelNameList[level] : "";
    }
    private static GameManager _instance;
    private List<string> levelPasswordList = new List<string> { };
    private List<string> levelNameList = new List<string> { };
    private void Awake()
    {
        if (Instance(this) != this) Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(this);
        }
    }
    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        bool hasPause = pauseMenu != null;
        bool hasTimer = timerDisplay != null;
        bool hasPlayer = player != null;
        if (ccAllowed && Input.GetKeyDown(KeyCode.C))
        {
            Console.instance.ToggleConsole();
            inConsole = !inConsole;
        }
        if (hasPause && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.TogglePause();
            inPause = !inPause;
        }
        if (hasPlayer && (isRunning || player.ignoreManager))
        {
            player.pm.Movement(new Vector3(moveX, moveY));
            player.ps.Aim();
            if (Input.GetMouseButtonDown(0)) // 0 for left click
            {
                player.ps.Shoot();
            }
        }
        if (isRunning && hasTimer)
        {
            timerDisplay.UpdateClock();
            clock += Time.deltaTime;
        }
    }
}