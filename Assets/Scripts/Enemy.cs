using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    //wavepointindex is the index number waypoint the enemy is targetting to move toward
    private int wavepointIndex = 0;

    void Start()
    {
        //Waypoints.points refers to the array built in waypoints script, always aiming at the first waypoint.
        target = waypoints.points[0];
    }

    void Update ()
    {
        //move close to the target x, y, z reference points in vector3
        //point from current position to the target
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //checking the position of itself (by using the transform position) and checking the target. If really close, then we change waypoint
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        //this is checking if the enemy has reached the last waypoint. If it has, it gets destroyed from the game. 
        if (wavepointIndex >= waypoints.points.Length -1)
        {
            Destroy(gameObject);
            //the return here is to stop the code from running the stuff outside of this if statement should the gameobject be deleted
            return;
        }

        wavepointIndex++;
        target = waypoints.points[wavepointIndex];
    }
}
