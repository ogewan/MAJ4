using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Transform player;
    public int zoffset;

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, zoffset);
    }
    private void Start()
    {
        var playerbase = FindObjectOfType<Player>();
        if (playerbase != null) player = playerbase.transform;
    }
}