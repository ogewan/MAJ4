using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class menuHandler : MonoBehaviour
{
    public Button newGameButton;
    
    void Start()
    {
        Button btn = newGameButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

	void TaskOnClick(){
        SceneManager.LoadScene(sceneName: "SampleScene");
	}
}