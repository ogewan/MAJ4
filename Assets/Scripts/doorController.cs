using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    public Transform leftHalf;
    public Transform rightHalf;

    public bool open;
    public bool playedOpenAnim = false;
    public bool playedCloseAnim = true;

    // Update is called once per frame
    void Update()
    {
        if (open && !playedOpenAnim)
        {
            OpenAnim();
            playedCloseAnim = false;
        }
        if (!open && !playedCloseAnim)
        {
            CloseAnim();
            playedOpenAnim = false;
        }
    }

    void OpenAnim()
    {
        Vector3 leftHalfPos = leftHalf.position + new Vector3(-leftHalf.localScale.x/2, -leftHalf.localScale.y/2, 0);
        Vector3 rightHalfPos = rightHalf.position + new Vector3(-rightHalf.localScale.x/2, -rightHalf.localScale.y/2, 0);
        for (float i = 2f; i > 0; i -= 0.001f)
        {
            leftHalf.localScale = new Vector3(i, leftHalf.localScale.y, leftHalf.localScale.z);
            leftHalf.localPosition = leftHalfPos + new Vector3(leftHalf.localScale.x/2, leftHalf.localScale.y/2, 0);
            rightHalf.localScale = new Vector3(i, rightHalf.localScale.y, rightHalf.localScale.z);
            rightHalf.localPosition = rightHalfPos + new Vector3(rightHalf.localScale.x/2, rightHalf.localScale.y/2, 0);
        }
        playedOpenAnim = true;
    }

    void CloseAnim()
    {
        Vector3 leftHalfPos = leftHalf.position + new Vector3(-leftHalf.localScale.x/2, -leftHalf.localScale.y/2, 0);
        Vector3 rightHalfPos = rightHalf.position + new Vector3(-rightHalf.localScale.x/2, -rightHalf.localScale.y/2, 0);
        for (float i = 0; i < 2f; i += 0.001f)
        {
            leftHalf.localScale = new Vector3(i, leftHalf.localScale.y, leftHalf.localScale.z);
            leftHalf.localPosition = leftHalfPos + new Vector3(leftHalf.localScale.x/2, leftHalf.localScale.y/2, 0);
            rightHalf.localScale = new Vector3(i, rightHalf.localScale.y, rightHalf.localScale.z);
            rightHalf.localPosition = rightHalfPos + new Vector3(rightHalf.localScale.x/2, rightHalf.localScale.y/2, 0);
        }
        playedCloseAnim = true;
    }
}
