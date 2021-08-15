using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuHandler : MonoBehaviour
{
    public int audioTrack;
    public Button[] levelButtons;
    public void NewGame()
    {
        SceneManager.LoadScene($"level0");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            var btn = levelButtons[i];
            var levelName = GameManager.instance.LoadLevel(i);
            btn.interactable = GameManager.instance.IsLevelUnlocked(i);
            btn.onClick.AddListener(() => SceneManager.LoadScene(levelName));
        }
    }
    private void Start()
    {
        GameManager.Instance();
        AudioManager.instance.PlayBGM(audioTrack);
    }
}