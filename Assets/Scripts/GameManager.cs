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
    public int playerHealth = 5;
    public Pause pauseMenu;
    public HealthUI hp;
    public bool ccAllowed;
    public static GameManager instance => Instance();
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
    public void TakeDamage()
    {
        playerHealth--;
        hp.ShowHealth(playerHealth);
    }
    public void AddHealth()
    {
        playerHealth++;
        hp.ShowHealth(playerHealth);
    }
    private static GameManager _instance;
    private void Start()
    {
        if (Instance(this) != this) Destroy(this);
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
        if (ccAllowed && Input.GetKeyDown(KeyCode.C))
        {
            Console.instance.ToggleConsole();
        }
        if (hasPause && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.TogglePause();
        }

        /*mousePos = Input.mousePosition;
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        angle = Mathf.Atan2(mousePos.y - objectPos.y, mousePos.x - objectPos.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (Input.GetMouseButtonDown(0)) // 0 for left click
            Instantiate(rb, ShootPosition.position, ShootPosition.rotation);*/
    }
}