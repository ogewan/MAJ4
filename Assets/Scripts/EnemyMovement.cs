using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 5f;
    private int currentWaypoint = 0;
    private float progression = 0;
    private int nextWaypoint = 1;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[currentWaypoint].position;
        currentWaypoint += nextWaypoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = waypoints[currentWaypoint].position * progression * Time.deltaTime;
        
        Vector3 dir = waypoints[currentWaypoint].position - transform.position
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);

        progression += speed / 100;

        if (currentWaypoint == waypoints.Count - 1)
            nextWaypoint = -1;
        if (currentWaypoint == 0)
            nextWaypoint = 1;

        if (progression >= 1)
            currentWaypoint += nextWaypoint;
    }
}
