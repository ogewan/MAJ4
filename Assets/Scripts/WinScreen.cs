using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public GameObject winWindow;
    public Text rankLeter;
    public Text score;
    public Text timer;
    public Text stars;
    public Text healthbonus;
    public Text timebonus;
    public Text total;
    public Image rankBack;
    public Image mainMenuBack;
    public Image retryBack;
    public Image continueBack;

    public void Report()
    {
        GameManager gm = GameManager.instance;
        TimeSpan time = TimeSpan.FromSeconds(gm.clock);
        Color rankColor = UnityEngine.Random.ColorHSV();
        rankLeter.text = gm.rank;
        score.text = $"Score: {gm.score}";
        timer.text = $"Time: {time.ToString(@"mm\:ss\:f")}";
        stars.text = $"Stars: {gm.special}";

        var hpbonus = Mathf.Clamp(gm.playerHealth * 15, 0, gm.maxHealth * 5);
        var tbonus = Mathf.RoundToInt(Mathf.Clamp(60 - gm.clock, 0, 60)) * 3;

        healthbonus.text = $"Health Bonus: {hpbonus}";
        timebonus.text = $"Time Bonus: {tbonus}";

        total.text = $"Total: {gm.score + hpbonus + tbonus}";

        rankBack.color = rankColor;
        mainMenuBack.color = rankColor;
        retryBack.color = rankColor;
        continueBack.color = rankColor;
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Continue()
    {
        GameManager.instance.Continue();
    }
    public void MainMenu()
    {
        GameManager.instance.MainMenu();
    }
    private void RegisterScreen()
    {
        GameManager.instance.win = this;
    }
    private void Start()
    {
        RegisterScreen();
    }
}