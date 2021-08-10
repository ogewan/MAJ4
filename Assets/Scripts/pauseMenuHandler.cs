using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuHandler : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    void Update()
    {
        bool isActive = pauseMenu.activeSelf;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!optionsMenu.activeSelf)
            {
                pauseMenu.SetActive(!isActive);
            } else
            {
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(true);
            }
        }
    }
}
