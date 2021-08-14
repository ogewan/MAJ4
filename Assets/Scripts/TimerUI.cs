using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Text timeText;
    public void UpdateClock()
    {
        float clock = GameManager.instance.clock;
        TimeSpan time = TimeSpan.FromSeconds(clock);
        timeText.text = time.ToString(@"mm\:ss\:f");
    }
    private void RegisterTimer()
    {
        GameManager.instance.timerDisplay = this;
    }
    private void Start()
    {
        RegisterTimer();
    }
}