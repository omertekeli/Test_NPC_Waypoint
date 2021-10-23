using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    public GameObject[] waypoints;
    
    int currentWP = 0;

    [SerializeField] float rotSpeed = 1.0f; //rotation speed
    [SerializeField] float speed = 3.0f; //forward speed
    [SerializeField] float accuracy = 1.0f; //how much close to target

    // Start is called before the first frame update
    void Start()
    {
        //Fill the array automatically.Another way, you can manual in editor with dragging.
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    // Update is called once per frame
    void Update()
    {
        //There is no waypoint, not work code after that look waypoints again
        if (waypoints.Length == 0) return;

        //Calculate new vector of current waypoint
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, transform.position.y, waypoints[currentWP].transform.position.z);

        Vector3 direction = lookAtGoal - transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        //compare the magnitude of vector and accuracy
        if (direction.magnitude < accuracy)
        {
            currentWP++; //pass next waypoint, if close the target enough
            //AI will move as much as the number of waypoints
            if (currentWP >= waypoints.Length)
            {
                currentWP = 0;
            }
        }

        //Move forward, if there is a waypoint
        this.transform.Translate(0, 0, speed * Time.deltaTime);


    }
}
