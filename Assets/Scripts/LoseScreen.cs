using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    public GameObject loseWindow;

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        GameManager.instance.MainMenu();
    }
    private void RegisterScreen()
    {
        GameManager.instance.lose = this;
    }
    private void Start()
    {
        RegisterScreen();
    }
}