using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;
    public void UpdateScore()
    {
        scoreText.text = $"{GameManager.instance.score}";
    }
    private void RegisterScoreOverlay()
    {
        GameManager.instance.scoreDisplay = this;
    }
    private void Start()
    {
        RegisterScoreOverlay();
    }
}