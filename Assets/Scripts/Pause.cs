using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool pauseActive = false;
    public GameObject pauseWindow;
    public GameObject optionWindow;
    public GameObject pauseBackground;
    public void TogglePause()
    {
        ControlPause(!pauseActive);
    }
    public void ControlPause(bool status)
    {
        pauseActive = status;
        GameManager.instance.inPause = status;
        pauseBackground.SetActive(pauseActive);
        pauseWindow.SetActive(pauseActive);
        optionWindow.SetActive(false);
    }
    public void OpenOptions()
    {
        optionWindow.SetActive(true);
        pauseWindow.SetActive(false);
    }
    public void Retry()
    {
        ControlPause(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        GameManager.instance.MainMenu();
    }
    private void RegisterPause()
    {
        GameManager.instance.pauseMenu = this;
        GameManager.instance.ReIndex();
    }
    //lazy dirty hack
    private void Update()
    {
        if (pauseActive && !optionWindow.activeInHierarchy && !pauseWindow.activeInHierarchy) pauseWindow.SetActive(true);
    }
    private void Start()
    {
        RegisterPause();
    }
}