using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class objectivesTextController : MonoBehaviour
{
    public List<string> objectives;
    public TextMeshProUGUI textMesh;

    private void Update()
    {
        textMesh.text = "";
        for (int i = 0; i < objectives.Count; i++)
        {
            textMesh.text = textMesh.text + (i + 1).ToString() + ". " + objectives[i] + "\n";
        }
    }
    private void Start()
    {
        RegisterObjectives();
    }
    private void RegisterObjectives()
    {
        GameManager.instance.objectivesDisplay = this;
    }
}