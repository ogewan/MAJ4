using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * GameManager.cs
 * Handles player update and game state: paused, console, level end
 * </summary>
 */
public class GameManager : MonoBehaviour
{
    public int level;
    public int playerHealth = 5;
    public float clock;
    public int score;
    public Player player;
    public Pause pauseMenu;
    public HealthUI healthDisplay;
    public ScoreUI scoreDisplay;
    public TimerUI timerDisplay;
    public bool ccAllowed;

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
    private static GameManager _instance;
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

        /*mousePos = Input.mousePosition;
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        angle = Mathf.Atan2(mousePos.y - objectPos.y, mousePos.x - objectPos.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (Input.GetMouseButtonDown(0)) // 0 for left click
            Instantiate(rb, ShootPosition.position, ShootPosition.rotation);*/
        if (isRunning && hasTimer)
        {
            timerDisplay.UpdateClock();
            clock += Time.deltaTime;
        }
    }
}