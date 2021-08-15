using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuHandler : MonoBehaviour
{
    public int audioTrack;
    //public Button[] levelButtons;
    public void LoadLevelWithGameObjectName()
    {
    }
    //WARNING: CURSED PROGRAMMING AHEAD
    public void LoadLevel_0()
    {
        SceneManager.LoadScene("level0");
    }
    public void LoadLevel_1()
    {
        SceneManager.LoadScene("level1");
    }
    public void LoadLevel_2()
    {
        SceneManager.LoadScene("level2");
    }
    public void LoadLevel_3()
    {
        SceneManager.LoadScene("level3");
    }
    public void LoadLevel_4()
    {
        SceneManager.LoadScene("level4");
    }
    public void LoadLevel_5()
    {
        SceneManager.LoadScene("level5");
    }
    public void LoadLevel_6()
    {
        SceneManager.LoadScene("level6");
    }
    public void LoadLevel_7()
    {
        SceneManager.LoadScene("level7");
    }
    public void LoadLevel_8()
    {
        SceneManager.LoadScene("level8");
    }
    public void LoadLevel_9()
    {
        SceneManager.LoadScene("level9");
    }
    public void LoadLevel_10()
    {
        SceneManager.LoadScene("level10");
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void Start()
    {
        GameManager.Instance();
        AudioManager.instance.PlayBGM(audioTrack);
    }
}