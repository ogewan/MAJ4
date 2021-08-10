using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 5f;

    public float threshold = 0.5f;

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

        Vector3 dir = waypoints[currentWaypoint].position - transform.position;
        dir.Normalize();
        transform.position = transform.position + dir * speed * Time.deltaTime;

        progression += speed / 100;


        if (currentWaypoint == waypoints.Count - 1)
            currentWaypoint = 0;

        float distance = Mathf.Sqrt(((transform.position.x - waypoints[currentWaypoint].position.x)*(transform.position.x - waypoints[currentWaypoint].position.x)) + ((transform.position.y - waypoints[currentWaypoint].position.y)*(transform.position.y - waypoints[currentWaypoint].position.y)));
        if (distance <= threshold)
            currentWaypoint += nextWaypoint;
    }
}
