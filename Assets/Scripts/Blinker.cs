using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    public TextMeshProUGUI blinker;
    private void Start()
    {
        StartCoroutine(Blinking());
    }
    private IEnumerator Blinking()
    {
        while (blinker.gameObject.activeInHierarchy)
        {
            blinker.enabled = false;
            yield return new WaitForSeconds(0.5f);
            blinker.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
}