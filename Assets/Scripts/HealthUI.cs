using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] healthIndicators;
    public Sprite overrideImage;

    public void ShowHealth(int hp)
    {
        hp = Mathf.Clamp(hp, 0, healthIndicators.Length);
        for (int i = 0; i < healthIndicators.Length; i++)
        {
            healthIndicators[i].gameObject.SetActive(hp <= i);
        }
    }
    private void RegisterHealthBar()
    {
        GameManager.instance.healthDisplay = this;
    }
    private void Start()
    {
        RegisterHealthBar();
    }
}