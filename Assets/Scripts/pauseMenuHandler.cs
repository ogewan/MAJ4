using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuHandler : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject pauseMenu;
    public GameObject background;
    public GameObject game;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                background.SetActive(false);
                game.SetActive(true);
            } else if (optionsMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                optionsMenu.SetActive(false);
            } else
            {
                pauseMenu.SetActive(true);
                background.SetActive(true);
                game.SetActive(false);
            }
        }
    }
}
