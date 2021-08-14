using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayUI : MonoBehaviour
{
    private void RegisterOverlay()
    {
        GameManager.instance.overlay = this;
        GameManager.instance.ReIndex();
    }
    private void Start()
    {
        RegisterOverlay();
    }
}