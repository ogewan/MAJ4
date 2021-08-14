using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int playerHealth;
    public int maxHealth;
    public float clock;
    public int score;
    public string rank;
    public List<string> objectives;
    public Player player;
    public Pause pauseMenu;
    public HealthUI healthDisplay;
    public ScoreUI scoreDisplay;
    public TimerUI timerDisplay;
    public OverlayUI overlay;
    public WinScreen win;
    public LoseScreen lose;
    public objectivesTextController objectivesDisplay;
    public bool ccAllowed;
    public bool consoleLoaded = false;
    public bool gameStart = false;
    public bool inPause = false;
    public bool inConsole = false;
    public List<string> keys = new List<string> { };
    public List<Door> doors = new List<Door> { };
    public static GameManager instance => Instance();
    public int special { get; set; } = 0;
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
        playerHealth = Mathf.Clamp(playerHealth + val, 0, maxHealth);
        healthDisplay.ShowHealth(playerHealth);
        if (playerHealth <= 0)
        {
            GameLose();
        }
    }
    public void ChangeScore(int val = 5)
    {
        score += val;
        scoreDisplay.UpdateScore();
    }
    public void DoorCheck()
    {
        foreach (var door in doors)
        {
            door.UnlockOnCheck();
        }
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
    public void SetRank()
    {
        var rankings = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var rankId = Random.Range(0, rankings.Length);
        rank = $"{rankings[rankId]}";
    }
    public void Continue()
    {
        SceneManager.LoadScene(level.nextLevelName.Length != 0 ? level.nextLevelName : "meinMenu");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("meinMenu");
    }
    public void GameWin()
    {
        GameEnd();
        win.winWindow.SetActive(true);
        win.Report();
    }
    public void GameLose()
    {
        GameEnd();
        player.gameObject.SetActive(false);
        Instantiate(Registry.instance.prefabs["Explosion"], player.transform.position, Quaternion.identity);
        lose.loseWindow.SetActive(true);
    }
    public void ReIndex()
    {
        if (overlay != null)
        {
            overlay.gameObject.transform.SetSiblingIndex(0);
            if (pauseMenu != null)
                pauseMenu.gameObject.transform.SetSiblingIndex(1);
        }
    }
    private static GameManager _instance;
    private List<string> levelPasswordList = new List<string> { };
    private List<string> levelNameList = new List<string> { };
    private void GameEnd()
    {
        gameStart = false;
        pauseMenu.ControlPause(false);
        Console.instance.ControlConsole(false);
        overlay.gameObject.SetActive(false);
        doors.Clear();
    }
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
        bool ctrl = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl);
        if (ccAllowed && ctrl)
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
            player.db.barrierTick();
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