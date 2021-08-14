using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public void enableCC(bool active)
    {
        GameManager.instance.ccAllowed = active;
    }
}