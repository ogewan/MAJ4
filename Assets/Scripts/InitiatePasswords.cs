using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiatePasswords : MonoBehaviour
{
    public List<string> levelPasswordList = new List<string> { };
    public List<string> levelNameList = new List<string> { };
    // Start is called before the first frame update
    private void Start()
    {
        var gm = GameManager.instance;
        gm.SetNameList(levelNameList);
        gm.SetPassList(levelPasswordList);
    }
}