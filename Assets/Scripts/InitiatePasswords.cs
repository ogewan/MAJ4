using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiatePasswords : MonoBehaviour
{
    public List<GameManager.LevelStatus> levelStatus = new List<GameManager.LevelStatus> { };
    // Start is called before the first frame update
    private void Start()
    {
        var gm = GameManager.instance;
        gm.SetLevelStatus(levelStatus);
    }
}