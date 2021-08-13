using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        pauseBackground.SetActive(pauseActive);
        pauseWindow.SetActive(pauseActive);
        optionWindow.SetActive(false);
    }
    public void OpenOptions()
    {
        optionWindow.SetActive(true);
        pauseWindow.SetActive(false);
    }
    private void RegisterPause()
    {
        GameManager.instance.pauseMenu = this;
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