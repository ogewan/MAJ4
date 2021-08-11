using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text score;
    private float scoreActual;
    void Update()
    {
        if(scoreActual > 0)
        {
            score.text = scoreActual.ToString();
        }
    }
    public void ChangeScore()
    {
        scoreActual += 5f;
    }
}
